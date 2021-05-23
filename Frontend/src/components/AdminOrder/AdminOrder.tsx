import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { IAdminOrder, IAdminOrderProducts } from '../../Models/IOrder';
import './AdminOrder.scss'

export default function AdminOrder() {
    const defaultOrderList:IAdminOrder[] = [];
    const [orderList, setOrderList] = useState(defaultOrderList);
    const [listLoaded, setListLoaded] = useState(true);
    let errorMessage = <h1>Ett fel inträffade, försök igen senare!</h1>;

    useEffect(() => {
       getOrders();
    }, [])


    async function getOrders(){
        try {
            let result = 
            await axios.get<IAdminOrder[]>("https://localhost:5001/api/order/");
    
            let updateOrderList:IAdminOrder[] = result.data.map((order: IAdminOrder) => {
                return order;
            });              
            setOrderList([...updateOrderList]);
            
        } catch (error) {
            setListLoaded(false);
        }
    }



    let orderListHtml = orderList.map((order: IAdminOrder, j:number) => {

         let orderProducts = order.orderProducts.map((item:IAdminOrderProducts, i:number) => {                       
           return(
                <li>
                    <div className="second-order-row">
                        <p>Artikelnr: {item.productId}</p>
                        <p>Artist: {item.product.artist}</p>
                        <p>Album: {item.product.album}</p>
                        <p>Antal: {item.amount}</p>
                    </div>
                    <div className="third-order-row">
                        <p>Färg: {item.color.name}</p>
                        <p>Format: {item.format.name}</p>
                        <p>Pris: {item.price}:-</p>
                    </div>
                    {i + 1 !== order.orderProducts.length ? <hr className="li-hr"/> : <React.Fragment/>}
                </li>
            )
        });
        return(
            <React.Fragment>
                <div key={order.id} className="order-row">
                    <div className="left-admin-section">
                        <p>Ordernr: {order.id}</p>
                        <p>Datum: {order.dateCreated.substring(0, 10)}</p>
                        <p>Betalsätt: {order.paymentMethod}</p>
                        <p>Kundnr: {order.customerId}</p>
                        <p>Total Summa: {order.totalPrice}:-</p>
                    </div>
                    <div className="right-admin-section">
                        <ul>
                            {orderProducts}
                        </ul>
                    </div>
                </div>
                {j + 1 !== orderList.length ? <hr/> : <React.Fragment/>}
            </React.Fragment>
        )
    });
    return (
        <div id="order-container">
        {orderListHtml}
        {!listLoaded ? errorMessage : <React.Fragment></React.Fragment>}
    </div>
    )
}
