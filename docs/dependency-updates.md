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

## How updates are grouped

Monthly, and per directory: one pull request carrying every minor and patch
bump, and one pull request per major. Minor and patch bumps rarely break
anything, so grouping them costs little and turns a weekly stream of single-bump
pull requests into one a month. Majors stay separate, so a major that breaks the
build blocks only itself while the group still lands.

`Microsoft.EntityFrameworkCore*` and `Microsoft.Extensions.*` are one family
group across all update types, listed before `minor-and-patch` so they match it
first: NU1605 is an error here, and a family member moving alone cannot restore.
EF Core stays below 10, which ships net10.0 assets only — see the comments in
`.github/dependabot.yml`.

## Why the vulnerability audit is not in CI

It answers a question about the global advisory database, not about the commit
under test. A newly published advisory turns every open pull request red at once
— including the bump that fixes it — and in a repository where green CI is what
merges updates, that stops updates from landing exactly when they matter most.
It runs weekly in `security-audit.yml`, blocks nothing, and writes its findings
into the run's summary and warnings rather than an issue. For NuGet it is the
only place transitive advisories show up: without a lock file the dependency
graph lists direct packages only, so Dependabot alerts cannot see them.

## How silence is broken

An unattended repository's real failure mode is not a bad merge, it is a queue
that quietly stops moving. Failing the run is not an option for that: these
workflows run against the default branch, so a red run pins a check to whatever
commit `main` points at, permanently. Each problem goes into an issue that its
own workflow opens and closes:

| Issue | Opened by | Opened when | Closed when |
|---|---|---|---|
| *Dependency updates are stuck* | the weekly sweep | a Dependabot pull request has sat unmerged for 14 days, or passed CI and the merge was refused | the first sweep that finds the queue moving |
| *Dependency promotion is blocked* | `deps-promote.yml` | the branch contract is violated, or the promotion cannot be opened | the first promotion run that is not blocked |

The two use different markers on purpose. They used to share one, and the sweep
kept closing an issue the promotion was still blocked on, which the next
promotion run reopened as a new issue — every day.

A run turns red only when its issue cannot be filed. The issues are opened
through `DEPS_PAT`, so under the owner's account: GitHub does not notify you of
your own actions, and they show up in the Issues tab rather than in the inbox.

## Known limits

- **Security updates are switched off.** `target-branch` is a version-update
  option; a fix raised from a Dependabot alert goes to the default branch
  whatever `dependabot.yml` says, and nothing in a repository can redirect it.
  Left on, they would put bot commits on `main` past the promotion, so they are
  disabled in the repository settings. Advisories still show in the Security tab
  and in `security-audit.yml`'s summary, and the monthly version updates carry
  most fixes through `deps` anyway.
- **The gate is only as good as CI.** `CI` green on a package bump means the
  solution restores, builds on both runners and the smoke test passes. There is
  no coverage of the WPF views beyond the screenshot job, so a bump that changes
  rendering or binding behaviour can land unnoticed. Auto-merge does not make the
  suite stronger; it makes its gaps land faster.
- **Actions are pinned to major tags, not commit SHAs.** A retargeted tag would
  execute in a job holding a write token. Pinning to SHAs closes that, and
  Dependabot still updates them; it is the obvious next hardening step.
- **The automation runs on a personal access token, not `GITHUB_TOKEN`.** See
  *The token* below. Two limits disappeared with it and are recorded here so
  nobody reintroduces them: `GITHUB_TOKEN` may not write `.github/workflows/`,
  which left every action bump unmergeable whenever it sat behind its base; and
  it could not ask for help either, because `@dependabot rebase` from
  `github-actions[bot]` is answered *"Sorry, only users with push access can use
  that command"*. The promotion also no longer parks a second CI entry at
  `action_required`, because it is opened by a real account.
- **A sweep can be cancelled while it is queued.** `concurrency` with
  `cancel-in-progress: false` keeps exactly one run waiting per group; during a
  burst of events the waiting one is cancelled by the next. Nothing is lost —
  every sweep re-reads state from the API rather than from the event — so a
  `cancelled` sweep in the run list is expected, not a fault.
- **The sweep keeps its own clock.** It stops at `DEADLINE_SECONDS`, inside the
  job's `timeout-minutes`, rather than waiting out a spent GraphQL budget until
  the job is killed: a killed job is reported as `cancelled` against whatever
  commit `main` points at.

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

1. **Settings → Secrets and variables → Actions**: `DEPS_PAT` — see *The token*.
   Without it both automation workflows fail immediately with 401.
2. **Settings → Actions → General → Workflow permissions**: *Allow GitHub
   Actions to create and approve pull requests* — ticked. Without it the
   promotion pull request cannot be opened and the step fails with 403.
   (The read-only default for `GITHUB_TOKEN` is fine: each workflow requests
   what it needs via its own `permissions:` block.)
3. **Settings → General → Pull Requests**: squash merging enabled.
4. **Settings → Advanced Security → Dependabot alerts**: enabled, so advisories
   show in the Security tab. **Dependabot security updates**: disabled — they
   target `main` directly and would bypass `deps`; see *Known limits*.

## Running it by hand

```
Actions → Dependency promotion → Run workflow   # creates/realigns deps, opens the promotion PR
Actions → Dependency auto-merge → Run workflow  # sweeps; one log line per open update
```

Both are safe to run repeatedly: they read state and act only where there is
something to do.
