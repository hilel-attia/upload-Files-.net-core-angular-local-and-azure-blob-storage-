// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'

import VueAlert from '@vuejs-pt/vue-alert'
import VeeValidate from 'vee-validate';

// Require the main Sass manifest file
require('./assets/sass/main.scss')

Vue.config.productionTip = false

const veeConfig = {
  events: 'input'
};

Vue.use(VeeValidate, veeConfig);
Vue.use(VueAlert);

const dict = {
  en: {
    custom: {
      fileUpload: {
        required: 'You must select a file to upload'
      }
    }
  }
};

VeeValidate.Validator.updateDictionary(dict);

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: { App }
})
