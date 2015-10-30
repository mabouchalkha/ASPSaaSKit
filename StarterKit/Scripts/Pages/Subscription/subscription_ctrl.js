angular.module('starterKit')
    .controller('subscriptionController',
    ['$http', '$state', 'notif', 'plans', 'subscriptions', 'stripe', function ($http, $state, notif, plans, subscriptions, stripe) {
    var vm = this;

    var _init = function () {
        vm.plans = plans.data;
        vm.subscriptions = subscriptions.data;

        vm.currentPlan = {};

       subscriptionViewModel = {
            SubscriptionPlanId: 0,
            StripeTokenId: ''
        };

        vm.card = {
            number: '',
            exp_month: '',
            exp_year: '',
            cvc:''
        }
    };

    vm.setPlan = function (plan) {
        vm.currentPlan.Name = plan.Name;
        vm.currentPlan.Interval = plan.Interval;
        vm.currentPlan.AmountInDollars = plan.AmountInDollars;
        subscriptionViewModel.SubscriptionPlanId = plan.Id;
        $state.go('subscription.billing');
    };

    vm.pay = function () {
        return stripe.card.createToken(vm.card)
      .then(function (response) {
          console.log('token created for card ending in ', response.card.last4);
          subscriptionViewModel.StripeTokenId = response.id;
          
          return $http.post('/Subscription/billing', subscriptionViewModel);
      })
      .then(function (subscription) {
          $state.go('subscription.profile');
      })
      .catch(function (err) {
          if (err.type && /^Stripe/.test(err.type)) {
              console.log('Stripe error: ', err.message);
          }
          else {
              console.log('Other error occurred, possibly with your API', err.message);
          }
      });
    };
    
    _init();
}]);
