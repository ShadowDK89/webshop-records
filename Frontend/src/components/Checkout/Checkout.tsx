import axios from 'axios';
import React, { useState } from 'react'
import { apiUrl } from '../../App';
import { ICart } from '../../Models/ICart';
import { ICustomer } from '../../Models/ICustomer';
import { IOrder, IOrderProducts } from '../../Models/IOrder';
import ExistingCustomer from '../ExistingCustomer/ExistingCustomer';
import NewCustomerForm from '../NewCustomerForm/NewCustomerForm';
import './Checkout.scss'

interface ICheckoutProps{
    totalPrice: number,
    cartInfo: ICart[]
}

export default function Checkout(props: ICheckoutProps) {
    const defaultSingleCustomer:ICustomer = {
        id: 0,
        firstName: "",
        lastName: "",
        adress: "",
        zipCode: "",
        city: ""
    };
    const [selectedCustomer, setSelectedCustomer] = useState(defaultSingleCustomer);
    const [showCustomerSelect, setShowCustomerSelect] = useState(false);
    const [showNewCustomerForm, setShowNewCustomerForm] = useState(false);
    const [showCheckoutForm, setShowCheckoutForm] = useState(false);
    const [orderSucceeded, setOrderSucceeded] = useState(false);
    const [userPayment, setUserPayment] = useState("");

    function isExistingCustomer(checkIfNewCustomer:boolean){
        if(checkIfNewCustomer){
            setShowCustomerSelect(true);
            setShowNewCustomerForm(false);
            setShowCheckoutForm(false);
        }
        else{
            setShowCustomerSelect(false);
            setShowCheckoutForm(false);
            setShowNewCustomerForm(true);
        }
        
    }

    function updateSelectedCustomer(customer:ICustomer){

        if(customer.id > 0){
            setSelectedCustomer(customer);
        }
        else{
            setSelectedCustomer(customer);
            setShowNewCustomerForm(!showNewCustomerForm);
            setShowCheckoutForm(!showCheckoutForm);
        }
        

    }

    function getPaymentMethod(paymentMethod:string){
        setUserPayment(paymentMethod);
        
    }

    function proceedToCheckout(){
        setShowCustomerSelect(!showCustomerSelect);
        setShowCheckoutForm(!showCheckoutForm);
    }

    function createNewOrder(){
        const getOrderProducts:IOrderProducts[] = props.cartInfo.map((product: ICart) => {
            return {
                productId: product.productId,
                amount: product.amount,
                price: product.price,
                colorId: product.colorId,
                formatId: product.formatId
            }
        });

        let newOrder:IOrder = {
            dateCreated: new Date(),
            paymentMethod: userPayment,
            totalPrice: props.totalPrice,
            customerId: selectedCustomer.id,
            orderProducts: getOrderProducts
        }
        if(newOrder.customerId === 0){
            addNewCustomer(newOrder);
        }else{
            sendOrder(newOrder);
        }
    }

    async function addNewCustomer(newOrder:IOrder) {
        let result = await axios.post(`${apiUrl}/customer/`, selectedCustomer);
        if(result.status === 201){
           newOrder.customerId = result.data.id
            sendOrder(newOrder);
        }
        
    }

    async function sendOrder(newOrder:IOrder){
        let result = 
        await axios.post<IOrder>(`${apiUrl}/order/`, newOrder);

        if(result.status === 201){
            setShowCheckoutForm(!showCheckoutForm);
            setOrderSucceeded(!orderSucceeded);
        }
    }

    let checkIfCustomerHtml = 
    <div>
        <h2>Är du en befintlig kund?</h2>
        <div id="current-customer-select">
            <button onClick={() => isExistingCustomer(true)}>Absolut!</button>
            <button onClick={() => isExistingCustomer(false)}>Nej, registrera!</button>
        </div>
    </div>

    let customerInputHtml = showCustomerSelect ?
    <ExistingCustomer
    updateSelectedCustomer={updateSelectedCustomer}
    sendPaymentMethod={getPaymentMethod}
    proceedWithOrder={proceedToCheckout}/> : 
    <React.Fragment></React.Fragment>;

    let registerNewCustomer = showNewCustomerForm ?
    <NewCustomerForm
    sendPaymentMethod={getPaymentMethod}
    sendToCheckout={updateSelectedCustomer}/> :
    <React.Fragment></React.Fragment>

    let customerInfoHtml =
    <div>
        <p>Förnamn: {selectedCustomer.firstName}</p>
        <p>Efternamn: {selectedCustomer.lastName}</p>
        <p>Adress: {selectedCustomer.adress}</p>
        <p>Postnr: {selectedCustomer.zipCode}</p>
        <p>Stad: {selectedCustomer.city}</p>
        <p>Betal metod: {userPayment.charAt(0).toUpperCase() + userPayment.slice(1)}</p>
    </div>

    let orderNowHtml = 
    <div>
        {customerInfoHtml}
        <h3>Kontrollera att rätt beställare står ovan</h3>
        <button onClick={createNewOrder}>Beställ</button>
    </div>

    let orderSentHtml = 
    <div>
        <h1>Din beställning har mottagits!</h1>
        <p>Vi skickar din order så fort alla varor är behandlade.</p>
    </div>

    return (
        <div>
            {checkIfCustomerHtml}
            {customerInputHtml}
            {registerNewCustomer}
            {showCheckoutForm ? orderNowHtml : <React.Fragment></React.Fragment>}
            {orderSucceeded ? orderSentHtml : <React.Fragment></React.Fragment>}
        </div>
    )
}
