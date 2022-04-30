import Vue from 'vue'
import Router from 'vue-router'
import FileUpload from '@/components/FileUpload'
import FileView from '@/components/FileView'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Upload',
      component: FileUpload
    },
    {
      path: '/upload/:id',
      name: 'File View',
      component: FileView
    }
  ]
})
