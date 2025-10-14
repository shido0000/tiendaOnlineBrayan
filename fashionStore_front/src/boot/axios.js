import { boot } from 'quasar/wrappers'
import axios from 'axios'
import { reactive } from 'vue'

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({
    baseURL: 'https://localhost:6005/api',
    headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' }
})

const apiDatosInicio = axios.create({
    baseURL: 'https://localhost:6005/',
    headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' }
})


const apiFotosBaseUrl = 'https://localhost:6005';
const apiFotos = axios.create({
    baseURL: apiFotosBaseUrl + '/',
    headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' }
})


const emp = reactive({
    empresa: null,
    empresaList: []
})
const sistemStatus = reactive({
    loading: false
})

export default boot(({ app }) => {
    // for use inside Vue files (Options API) through this.$axios and this.$api

    app.config.globalProperties.$axios = axios
    // ^ ^ ^ this will allow you to use this.$axios (for Vue Options API form)
    //       so you won't necessarily have to import axios in each vue file

    app.config.globalProperties.$api = api
    // ^ ^ ^ this will allow you to use this.$api (for Vue Options API form)
    //       so you can easily perform requests against your app's API

    app.config.globalProperties.$emp = emp
    app.config.globalProperties.$sistemStatus = sistemStatus
})

export { api, emp, sistemStatus, apiFotos, apiFotosBaseUrl, apiDatosInicio }
