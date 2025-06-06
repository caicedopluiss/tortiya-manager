import moment from "moment";
import type { NewOrder, Order } from "../../types/Order";
import axiosHttpClient from "../axiosHttpClient";
import type { OrderItem } from "../../types/OrderItem";
import parseDateToIso8601String from "../../utils/parseDateToIso8601String";

const baseUrl = "v1/orders";

type GetOrdersByDateRespone = {
    orders: Order[];
};
export async function getOrdersByDate(date: Date): Promise<GetOrdersByDateRespone> {
    const url = `${baseUrl}/${parseDateToIso8601String(date)}`;
    const response: GetOrdersByDateRespone = await axiosHttpClient.get(url);
    // Transform incoming dates string in utc to local date string
    return { ...response, orders: response.orders.map((x) => ({ ...x, date: moment(x.date).format() })) };
}

type PostOrderResponse = {
    order: Order;
};
export async function postOrder(order: Order) {
    const url = baseUrl;
    const newOrder: NewOrder = {
        clientDate: order.date,
        paymentMethod: order.paymentMethod,
        items: order.items.map((x: OrderItem) => ({
            name: x.name,
            quantity: x.quantity,
            cost: x.cost,
            charge: x.charge,
        })),
    };
    const response: PostOrderResponse = await axiosHttpClient.post(url, { order: newOrder });

    // Transform incoming dates string in utc to local date string
    return { ...response, order: { ...response.order, date: moment(order.date).format() } };
}
