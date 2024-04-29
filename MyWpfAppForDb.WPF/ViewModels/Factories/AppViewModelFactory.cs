using MyWpfAppForDb.WPF.State.Navigators;
using System;

namespace MyWpfAppForDb.WPF.ViewModels.Factories
{
	public class AppViewModelFactory : IAppViewModelFactory
	{
		private readonly CreateViewModel<HomeVM> _createHomeViewModel;
		private readonly CreateViewModel<ProductsVM> _createProductsViewModel;
		private readonly CreateViewModel<ProfileVM> _createProfileViewModel;
		private readonly CreateViewModel<StatisticsVM> _createStatisticsViewModel;
		private readonly CreateViewModel<YourDeliveryInfoVM> _createYourDeliveryInfoViewModel;
		private readonly CreateViewModel<AuthorizationVM> _createAuthorizationViewModel;
		private readonly CreateViewModel<RegistrationVM> _createRegistrationViewModel;

		public AppViewModelFactory(CreateViewModel<HomeVM> createHomeViewModel,
			CreateViewModel<ProductsVM> createProductsViewModel,
			CreateViewModel<ProfileVM> createProfileViewModel,
			CreateViewModel<StatisticsVM> createStatisticsViewModel,
			CreateViewModel<YourDeliveryInfoVM> createYourDeliveryInfoViewModel,
			CreateViewModel<AuthorizationVM> createAuthorizationViewModel,
			CreateViewModel<RegistrationVM> createRegistrationViewModel)
		{
			_createHomeViewModel = createHomeViewModel;
			_createAuthorizationViewModel = createAuthorizationViewModel;
			_createProductsViewModel = createProductsViewModel;
			_createProfileViewModel = createProfileViewModel;
			_createStatisticsViewModel = createStatisticsViewModel;
			_createYourDeliveryInfoViewModel = createYourDeliveryInfoViewModel;
			_createRegistrationViewModel = createRegistrationViewModel;
		}

		public ViewModelBase CreateViewModel(ViewType viewType)
		{
			switch (viewType)
			{
				case ViewType.Authorization:
					return _createAuthorizationViewModel();
				case ViewType.Home:
					return _createHomeViewModel();
				case ViewType.Registration:
					return _createRegistrationViewModel();
				case ViewType.Profile:
					return _createProfileViewModel();
				case ViewType.Statistics:
					return _createStatisticsViewModel();
				case ViewType.YourDeliveryInfo:
					return _createYourDeliveryInfoViewModel();
				case ViewType.Products:
					return _createProductsViewModel();
				default:
					throw new ArgumentException
						("The ViewType does not have a ViewModel.", "viewType");
			}
		}
	}
}
