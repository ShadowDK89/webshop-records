import React, { useState } from 'react'
import { ICart } from '../../Models/ICart'
import Checkout from '../Checkout/Checkout';
import './Cart.scss'

interface ICartProps{
    Products:ICart[]
}

export default function Cart(props: ICartProps) {
    const [openCheckout, setOpenCheckout] = useState(false);
    let totalPrice = 0;

    for (let index = 0; index <props.Products.length; index++) {
        totalPrice += props.Products[index].price;
        
    }

    function toCheckout(){
        setOpenCheckout(!openCheckout);
    }

    let userCartHtml = props.Products.map((item: ICart) => {
        return(
            <div key={item.productId} className="cart-item">
                <img src={item.imgUrl} alt=""/>
                <div className="cart-info">
                    <h3>{item.artist}</h3>
                    <p>{item.album}</p>
                    <div className="extra-info">
                        <div>
                        <span>Format: </span><p> {item.formatName}</p>
                        </div>
                        <div>
                        <span>Färg: </span><p>{item.colorName}</p>
                        </div>
                    </div>
                    <p>Antal: <span>{item.amount}</span></p>
                </div>
                <p>{item.price} :-</p>
            </div>
        )
    });

    let emptyCartHtml = <p>Här var det tomt</p>;
    let showPrice = totalPrice === 0 ? <React.Fragment></React.Fragment> : <p>Totalt: {totalPrice}:-</p>;

    
    let checkoutBtn = props.Products.length !== 0 && !openCheckout ?
    <button id="checkout-btn" type="button" onClick={toCheckout}>Till Kassan</button> : <React.Fragment></React.Fragment>;

    let cartContainer = openCheckout ?
    <Checkout totalPrice={totalPrice} cartInfo={props.Products}></Checkout> : userCartHtml;

    return (
        <div id="cart-container">
            {props.Products.length === 0 ? emptyCartHtml : cartContainer}
            {showPrice}
            {checkoutBtn}
        </div>
    )
}
