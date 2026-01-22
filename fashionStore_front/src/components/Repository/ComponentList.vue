<template>
  <q-list bordered class="generic-list">
    <q-item
      v-for="(item, index) in items"
      :key="index"
      clickable
      v-ripple
      @click="handleItemClick(item)"
    >
      <q-item-section avatar>
        <q-icon :color="item.color || 'primary'" :name="item.icon" />
      </q-item-section>

      <q-item-section>
        {{ item.title }}
      </q-item-section>

      <q-item-section side>
        <q-icon name="chevron_right" color="grey-8" />
      </q-item-section>
    </q-item>
  </q-list>
</template>

<script setup>
import { defineProps, defineEmits } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const emit = defineEmits(['item-click'])

const props = defineProps({
  items: {
    type: Array,
    required: true,
    validator: (arr) =>
      arr.every(
        (item) =>
          item.title && item.icon && (item.link || item.action)
      ),
  },
})

function handleItemClick(item) {
  if (item.action) {
    emit('item-click', item.action)
  } else if (item.link) {
    router.push(item.link)
  }
}
</script>

<style scoped lang="scss">
.generic-list {
  border-radius: 8px;

  :deep(.q-item) {
    padding: 12px 16px;
    transition: background-color 0.2s ease;

    &:hover {
      background-color: rgba(0, 0, 0, 0.02);
    }

    &:active {
      background-color: rgba(0, 0, 0, 0.05);
    }
  }

  :deep(.q-item__section--avatar) {
    min-width: 40px;

    .q-icon {
      font-size: 24px;
    }
  }
}
</style>
