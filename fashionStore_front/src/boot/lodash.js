import { boot } from 'quasar/wrappers'
import lodash from 'lodash'

export default boot(async ({ app }) => {
  app.use(lodash)
})
