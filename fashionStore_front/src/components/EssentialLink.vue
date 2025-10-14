<template>
    <div
        class="q-pa-md"
        style="max-width: 350px; font-weight: 700; color: #205072"
    >
        <q-list padding class="rounded-borders">
            <div v-for="(link, index) in essentialListUrl" :key="index">
                <q-item
                    class="navbar-late q-pl-none text-grey-8"
                    active-class="navbar-active"
                    v-if="!link.children"
                    clickable
                    :key="link.id"
                    :to="{ name: link.link }"
                    expand-separator
                >
                    <q-item-section class="q-ml-md" avatar>
                        {{ link?.title }}
                    </q-item-section>
                </q-item>
                <q-expansion-item
                    group="man"
                    class="text-grey-8"
                    v-if="link.children"
                    :content-inset-level="0.5"
                    expand-separator
                    :icon="link.icon"
                    :label="link?.title"
                >
                    <div
                        v-for="(son, index) in link.children"
                        :key="index"
                        style="border-radius: 5px"
                    >
                        <q-item
                            v-if="!son.children"
                            :to="{ name: son.link }"
                            v-ripple
                            clickable
                            :icon="son.icon"
                        >
                            <q-item-section avatar>
                                <q-icon size="sm" :name="son.icon" />
                            </q-item-section>
                            <q-item-section>
                                <q-item-label class="text-grey-8">{{
                                    son?.title
                                }}</q-item-label>
                            </q-item-section>
                        </q-item>
                        <q-expansion-item
                            group="manp"
                            class="q-px-none text-grey-8 prueba"
                            v-if="son.children"
                            :content-inset-level="0.5"
                            expand-separator
                            :icon="son.icon"
                            :label="son?.title"
                        >
                            <div
                                v-for="(son1, index) in son.children"
                                :key="index"
                            >
                                <q-item
                                    v-if="!son1.children"
                                    :to="{ name: son1.link }"
                                    v-ripple
                                    clickable
                                    :icon="son1.icon"
                                >
                                    <q-item-section avatar>
                                        <q-icon size="sm" :name="son1.icon" />
                                    </q-item-section>
                                    <q-item-section>
                                        <q-item-label class="text-grey-8">{{
                                            son1?.title
                                        }}</q-item-label>
                                    </q-item-section>
                                </q-item>
                            </div>
                        </q-expansion-item>
                    </div>
                    <q-separator />
                </q-expansion-item>
            </div>
        </q-list>
    </div>
</template>
<script setup>
import { essentialListUrl } from "src/assets/js/util/essentialListUrl";
import { ref, onMounted } from "vue";

onMounted(async () => {

});
</script>

<style scoped>
.q-router-link--active {
    background-color: #fbe9e7;
}
</style>
