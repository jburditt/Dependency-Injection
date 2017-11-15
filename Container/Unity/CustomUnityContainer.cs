using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace Container.Unity
{
    /// <summary>
    /// wrapper for unity resolution.
    /// </summary>
    public class CustomUnityContainer : UnityContainer, IContainer<CustomUnityContainer>, IResolver
    {
        public T ResolveInstance<T>() where T : class
        {
            return this.Resolve<T>();
        }

        public void RegisterInstance(Type type, object instance, Lifetime lifetime = Lifetime.Default)
        {
            UnityContainerExtensions.RegisterInstance(this, type, instance);
        }

        public void RegisterInstance<TService, TImplementation>(object instance, Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService
        {
            UnityContainerExtensions.RegisterInstance(this, typeof(TService), instance);
        }

        public void RegisterInstance<TService, TImplementation>(Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService
        {
            switch (lifetime)
            {
                case Lifetime.Scoped:
                    this.RegisterType<TService, TImplementation>(new PerThreadLifetimeManager());
                    break;
                case Lifetime.Singleton:
                    this.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
                    break;
                case Lifetime.Transient:
                    this.RegisterType<TService, TImplementation>(new TransientLifetimeManager());
                    break;
                case Lifetime.Default:
                default:
                    this.RegisterType<TService, TImplementation>();
                    break;
            }
        }

        public void RegisterDependencies(ContainerSettings settings)
        {
            BaseContainer.RegisterDependencies(this);

            this.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));            
        }

        public IContainer<CustomUnityContainer> PostRegister() {
            return this;
        }

        public IContainer<CustomUnityContainer> VerifyContainer() {
            return this;
        }

        ////// TODO Refactor this out of CustomUnityContainer
        //public void Configure_Test_GiftCards()
        //{
        //    var giftCardService = Substitute.For<IGiftCardService>();
        //    giftCardService.CardPreAuth(null, null).ReturnsForAnyArgs(new GiftCardResponse { GiftCardResponseModel = new GiftCardResponseModel { ResponseCode = "01" } });

        //    // TODO Get one gift card to fail
        //    //var cardRequest = CheckoutData.GiftCard.ToCardRequest();
        //    //cardRequest = SetupRequestModel(cardRequest, CountryCode.CA);
        //    //_giftCardService.CardPreAuth(Arg.Any<ServiceModel>(), cardRequest).Returns(new GiftCardResponse { GiftCardResponseModel = new GiftCardResponseModel { ResponseCode = "01" } });
        //    //_giftCardService.CardPreAuth(Arg.Any<ServiceModel>(), cardRequest).Returns(new GiftCardResponse { GiftCardResponseModel = new GiftCardResponseModel { ResponseCode = "01" } });
        //    //cardRequest = CheckoutData.FailGiftCard.ToCardRequest();
        //    //cardRequest = SetupRequestModel(cardRequest, CountryCode.CA);
        //    //_giftCardService.CardPreAuth(Arg.Any<ServiceModel>(), cardRequest).Returns(new GiftCardResponse { GiftCardResponseModel = new GiftCardResponseModel { ResponseCode = "15" } });

        //    this.RegisterInstance(giftCardService);

        //    var cartLogic = Substitute.For<ICartLogic>();
        //    cartLogic.List(null).ReturnsForAnyArgs(CheckoutData.Cart);
        //    this.RegisterInstance(cartLogic);

        //    //var checkoutLogic = Substitute.For<ICheckoutLogic>();
        //    //checkoutLogic.GetOrder(null).ReturnsForAnyArgs(CheckoutData.Order_WithGiftCard);
        //    //this.RegisterInstance(checkoutLogic);

        //    var unitOfWork = Substitute.For<IUnitOfWork>();
        //    unitOfWork.TableStorageRepository.Get<OrderModel>((long)1).ReturnsForAnyArgs(CheckoutData.Order_WithGiftCard);
        //    this.RegisterInstance(unitOfWork);
        //}
    }
}