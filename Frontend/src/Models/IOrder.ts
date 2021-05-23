import { IColor } from "./IColor";
import { IFormat } from "./IFormat";
import { IProduct } from "./IProduct";

export interface IOrder{
    dateCreated: Date,
    paymentMethod: string,
    totalPrice: number,
    customerId: number,
    orderProducts: IOrderProducts[]
}


export interface IOrderProducts{
    productId: number,
    amount: number,
    price: number,
    colorId: number,
    formatId: number
}

export interface IAdminOrder{
    id: number,
    dateCreated: string,
    paymentMethod: string,
    totalPrice: number,
    customerId: number,
    orderProducts: IAdminOrderProducts[],
}

export interface IAdminOrderProducts{
    productId: number,
    amount: number,
    price: number,
    colorId: number,
    formatId: number,
    product: IProduct,
    color: IColor,
    format: IFormat
}