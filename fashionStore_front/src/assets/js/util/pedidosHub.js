import * as signalR from "@microsoft/signalr";
import { Notify } from "quasar";
import { ref } from "vue";

export const pedidos = ref([]);

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://tuservidor/pedidosHub", {
        accessTokenFactory: () => localStorage.getItem("token")
    })
    .withAutomaticReconnect()
    .build();

connection.on("PedidoGenerado", (pedido) => {
    // 🔴 Notificación visual
    Notify.create({
        message: `Nuevo pedido #${pedido.PedidoId} - Total: $${pedido.Total}`,
        color: "green",
        position: "top-right",
        timeout: 5000,
        icon: "shopping_cart"
    });

    // 🔴 Actualizar lista reactiva
    pedidos.value.push(pedido);
});

export async function iniciarConexionPedidos() {
    try {
        await connection.start();
        console.log("Conectado al hub de pedidos");
    } catch (err) {
        console.error("Error al conectar con SignalR:", err);
    }
}
