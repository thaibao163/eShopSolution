import * as cuid from "cuid"

export interface BasketItem {
    // cartDetailId: number
    productId: number
    // carId: number
    productName: string
    price: number
    quantity: number
    // toTal: number
}

export interface Basket{
    id:string;
    items:BasketItem[];
}

export class Basket implements Basket {
    id = cuid();
    items: BasketItem[] = [];
}

export interface IBasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
}