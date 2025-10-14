<template>
  <router-view />
</template>
<script>
import { watchEffect } from 'vue'
import { emp, sistemStatus } from 'src/boot/axios'


export default {
  name: 'App',
  setup () {
    watchEffect(() => {
      if (emp.empresa?.id !== null && emp.empresa?.id !== undefined) {

      }
      if (emp.empresa?.id !== null && emp.empresa?.id !== undefined) {

      }
    })
    return {
      emp,
      sistemStatus
    }
  }
}
</script>
