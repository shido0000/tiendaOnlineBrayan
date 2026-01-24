import { boot } from 'quasar/wrappers'
import { vHasRole } from 'assets/js/directives/vHasRole'

export default boot(({ app }) => {
    // Registrar la directiva globalmente
    app.directive('has-role', vHasRole)
})
