import {reactive} from 'vue';

export const EventBus = reactive({
  events: {},

  /**Emite un evento con el nombre especificado y una carga útil (payload).*/
  emit(event, payload) {
    if (this.events[event]) {
      this.events[event].forEach(callback => callback(payload));
    }
  },

  /**Registra un oyente para el evento especificado*/
  on(event, callback) {
    if (!this.events[event]) {
      this.events[event] = [];
    }
    this.events[event].push(callback);
  },

  /**Elimina el oyente para el evento especificado.*/
  off(event, callback) {
    if (this.events[event]) {
      this.events[event] = this.events[event].filter(cb => cb !== callback);
    }
  }
});

