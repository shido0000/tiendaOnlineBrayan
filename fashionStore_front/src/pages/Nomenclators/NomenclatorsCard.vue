<template class="justify-center">
    <!-- Vista móvil - Lista -->
    <div class="mobile-view q-pa-md">
        <ComponentList :items="cardsNomencladores" />
    </div>

    <!-- Vista desktop - Tarjetas -->
    <div class="desktop-view q-pt-xs q-ma-xl row justify-center item-center">
        <div
            class="tamaño-card text-center col-xs-12 col-sm-4 col-md-2 col-xl-2 justify-center q-mx-sm"
            style="width: 200px"
            v-for="(card, index) in cardsNomencladores"
            :key="index"
        >
            <p class="text-black"></p>
            <MenuCards
                :title="card.title"
                :icon="card.icon"
                :link="card.link"
                :color="card.color"
            />
        </div>
    </div>
</template>

<script setup>
import MenuCards from "src/components/cards/MenuCards.vue";
import ComponentList from "src/components/Repository/ComponentList.vue";
import { cardsNomencladores } from "src/assets/js/linksCard/linkCards";
</script>

<style scoped lang="scss">
/* Vista móvil - mostrar solo en pantallas pequeñas */
.mobile-view {
    display: block;

    @media (min-width: 600px) {
        display: none;
    }
}

/* Vista desktop - mostrar solo en pantallas medianas y grandes */
.desktop-view {
    display: none;

    @media (min-width: 600px) {
        display: flex;
    }
}
</style>
