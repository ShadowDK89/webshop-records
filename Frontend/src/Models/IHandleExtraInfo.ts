import { IProductColor } from "./IProductColor";
import { IProductFormat } from "./IProductFormat";

//For handling extra info that a customer chooses for a product
export interface IHandleExtraInfo{
    productColor: IProductColor,
    productFormat: IProductFormat
}