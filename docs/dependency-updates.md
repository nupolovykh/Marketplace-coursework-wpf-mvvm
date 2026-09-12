# Dependency updates

Updates land on `deps` without a human and reach `main` through one reviewed
pull request. Built for an **archived** project: nobody is watching, so the
design optimises for "keeps working unattended" over "clever".

```
Dependabot ─▶ PR ─▶ CI ──green on this exact commit──▶ squash-merged into deps
                                                              │
                                          promotion PR (CI runs on the result)
                                                              │
                                                       ◀ human merges ▶
                                                              ▼
                                                             main
                                                              │
                                        deps re-cut from main ┘
```

## Branch contract

| Branch | Who writes | History |
|---|---|---|
| `main` | humans only | amend-only — the archive's history stays flat |
| `deps` | bots only (Dependabot, Actions) | **disposable**, re-cut from `main` after every promotion |

`deps` holding nothing but bot commits is not a style rule, it is what makes the
rest work — see below.

## Why `deps` is reset, never merged

The usual design keeps the integration branch level by merging `main` into it.
That design breaks: `deps` exists to change lockfiles, `main` changes lockfiles,
and lockfiles conflict on nearly every line. GitHub answers `409 Merge conflict`,
no token changes that, and a human has to resolve a generated file by hand.

`deps` does not need merging, because **every bot commit is regenerable**. Reset
the branch and Dependabot re-reads the manifest, sees the old versions, and
raises the same bumps on its next scan. So the branch is thrown away and re-cut
from `main` instead of merged into. No merge, no conflict, no human.

The guard in `deps-promote.yml` is what keeps that assumption true: one non-bot
commit on `deps` and the workflow resets nothing and turns red. The single
exception is a commit that is provably `main`'s own amended-away tip (detected
via `github.event.before` on the force-push), which is debris, not work.

Every state the two branches can be in is handled. *Adds content* below is one
precise question: of the paths `deps` changed relative to the commit the two
branches split from, is there one where `main` does not already hold `deps`'s
content? It is answered by comparing blob SHAs in three trees — the split point,
`main`'s tip and `deps`'s tip — because a tree is addressed by its own SHA and
cannot be read stale, unlike the compare endpoint:

| `main` vs `deps` | Cause | Action |
|---|---|---|
| identical | steady state | nothing |
| ahead, adds content | updates collected | open/refresh the promotion PR |
| ahead, adds nothing | one full cycle's leftover merge | reset `deps` |
| behind | promotion merged with a merge commit | fast-forward `deps` |
| diverged, adds nothing | promotion squash-merged, or `main` moved on | reset `deps` |
| diverged, adds content, promotion open | `main` moved on under real bumps | merge `main` in through `update-branch`, keep the bumps |
| diverged, adds content, no promotion | same, nothing to merge through | open the promotion as it stands |
| diverged, amended tip | `git commit --amend` on `main` | reset `deps`, bumps re-raised |
| diverged, real human commit | someone pushed to `deps` | refuse, run turns red |

The `update-branch` row is the one that is easy to get wrong. Treating every
divergence as a rewrite and re-cutting the branch means the update queue
restarts from nothing on every single commit to `main`.

## Why there is no checkout

Neither automation workflow checks out the repository — every step is a `gh api`
call. There is no working tree and no on-disk script for a push to `deps` to
poison, so a job holding `contents: write` can never be made to execute code
that came from the branch it writes to. This is why no `ref:` pin is needed:
the class of problem it defends against does not exist here.

## Why updates are ungrouped

Grouping trades away the property that matters most when nobody is watching.
One pull request per bump means a bump that breaks the build blocks only itself
and the rest still land. Inside a group, one bad member holds back every good
one until a human splits it out. Dependabot rebases the losers of a lockfile
race by itself, so the queue drains without help — measured on this repository:
nine updates, two days, zero left open.

## Why the vulnerability audit is not in CI

It answers a question about the global advisory database, not about the commit
under test. A newly published advisory turns every open pull request red at once
— including the bump that fixes it — and in a repository where green CI is what
merges updates, that stops updates from landing exactly when they matter most.
It runs weekly in `security-audit.yml` and blocks nothing.

## How silence is broken

An unattended repository's real failure mode is not a bad merge, it is a queue
that quietly stops moving. The only notification that reaches anyone is a failed
workflow run, which GitHub emails to the repository owner. So:

- the weekly sweep **exits non-zero** when a Dependabot pull request has been
  open and unmerged for 14 days, or when one passed CI and the merge was refused
  (that one is the automation's own fault and fails immediately);
- `deps-promote.yml` exits non-zero when the branch contract is violated;
- `security-audit.yml` turns red on a new high-severity advisory.

A green run genuinely means nothing needs attention.

## Known limits

- **Security updates never reach `deps`.** `target-branch` is a version-update
  option; a fix raised from a Dependabot alert goes to the default branch
  whatever `dependabot.yml` says. Nothing in a repository can redirect it. The
  sweep lists those pull requests and turns red once they are 14 days old, so
  they cannot be invisible — but a human merges them. This is also why the
  branch is called `deps` and not `security`: security updates are the one kind
  it does not carry.
- **The gate is only as good as CI.** `CI` green on a package bump means the
  solution restores, builds on both runners and the smoke test passes. There is
  no coverage of the WPF views beyond the screenshot job, so a bump that changes
  rendering or binding behaviour can land unnoticed. Auto-merge does not make the
  suite stronger; it makes its gaps land faster.
- **Actions are pinned to major tags, not commit SHAs.** A retargeted tag would
  execute in a job holding a write token. Pinning to SHAs closes that, and
  Dependabot still updates them; it is the obvious next hardening step.
- **The promotion pull request carries a second, parked CI entry.** It is opened
  by `GITHUB_TOKEN`, so GitHub registers a `pull_request` run for it and holds it
  at `action_required` — *"1 workflow awaiting approval"*. It never turns green on
  its own and there is no reason to approve it: checks attach to a commit, not to
  a pull request, so the run this workflow dispatches on `deps`'s head is the real
  one and shows up as the pull request's own green CI. **The merge itself is not
  blocked** — the button is live and the banner says so. To make the extra entry
  disappear, set a repository secret `DEPS_TOKEN` to a personal access token: the
  pull request then comes from a real account, CI runs on it normally, and the
  dispatch becomes unnecessary. Nothing requires it.
- **`GITHUB_TOKEN` cannot write `.github/workflows/`.** A Dependabot pull request
  that edits a workflow and is behind its base cannot be merged by the gate, so
  it comments `@dependabot rebase` once and Dependabot, which has the
  permission, brings the head level.

## The token

`dependabot-auto-merge.yml` and `deps-promote.yml` authenticate as the repository
secret `DEPS_PAT`. Nothing else does.

That split is what makes a personal access token acceptable. Those two do no
checkout at all — every step is an API call, so there is no working tree, no
script on disk that a push to `deps` could poison, and no package restore that
could read the environment. Grep for `actions/checkout` in either file and the
count is zero. Adding a checkout step to a workflow holding `DEPS_PAT` hands the
token to whatever the update being tested chooses to run.

`GITHUB_TOKEN` was replaced because it may not write `.github/workflows/`, which
left every action bump unmergeable whenever it sat behind its base, and it could
not ask for help either: `@dependabot rebase` from `github-actions[bot]` is
answered *"Sorry, only users with push access can use that command"*.

## Why the screenshot job is fenced off from `deps`

`ci.yml`'s `publish-screenshots` commits generated screenshots under a human
name and pushes them to the ref it ran on. On `deps` that is exactly the commit
the promotion's guard refuses to reset — correctly, because the guard cannot
tell that commit from someone's real work. The job is therefore skipped when the
target is `deps` or the pull request is Dependabot's. Loosening the guard
instead would have removed the only thing protecting the branch contract.

## Repository settings this depends on

Not in the repository, so listed here:

1. **Settings → Actions → General → Workflow permissions**: *Allow GitHub
   Actions to create and approve pull requests* — ticked. Without it the
   promotion pull request cannot be opened and the step fails with 403.
   (The read-only default for `GITHUB_TOKEN` is fine: each workflow requests
   what it needs via its own `permissions:` block.)
2. **Settings → General → Pull Requests**: squash merging enabled.

## Running it by hand

```
Actions → Dependency promotion → Run workflow   # creates/realigns deps, opens the promotion PR
Actions → Dependency auto-merge → Run workflow  # sweeps; one log line per open update
```

Both are safe to run repeatedly: they read state and act only where there is
something to do.
