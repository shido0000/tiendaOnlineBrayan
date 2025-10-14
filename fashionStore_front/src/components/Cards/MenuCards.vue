<template class="justify-center">
    <q-card
        :class="{ 'bg-green text-white': props.color === 'green' }"
        class="card text-primary justify-center cursor-pointer"
        @click="handleClick"
    >
        <q-card-section class="pp q-mt-sm justify-center">
            <div class="q-gutter-md">
                <div class="icon-container">
                    <q-icon
                        class="ico q-pa-sm"
                        size="2.5rem"
                        :name="props.icon"
                    />
                </div>
                <div class="text-container q-mt-md q-pl-xs">
                    <q-card-section
                        class="li justify-center text q-pl-none card-title"
                        style="height: 120px"
                        >{{ props.title }}
                    </q-card-section>
                </div>
            </div>
        </q-card-section>
    </q-card>
</template>

<script setup>
import { defineProps, defineEmits } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const emit = defineEmits(["action"]);

const props = defineProps({
    title: {
        type: String,
        required: true,
    },
    icon: {
        type: String,
        required: true,
    },
    link: {
        type: String,
        required: false,
    },
    action: {
        type: String,
        required: false,
    },
    color: {
        type: String,
        required: false,
        default: "",
    },
});

function handleClick() {
    if (props.action) {
        emit("action", props.action);
    } else if (props.link) {
        router.push(props.link);
    }
}
</script>
