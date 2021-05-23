import React, { ChangeEvent, useState } from 'react'
import { ICustomer } from '../../Models/ICustomer';
import './NewCustomerForm.scss'

interface INewCustomerForm{
    sendPaymentMethod(paymentMethod: string):void;
    sendToCheckout(customerInfo:ICustomer):void;
}

export default function NewCustomerForm(props: INewCustomerForm) {
    const defaultCustomer:ICustomer = {
        id: 0,
        firstName: "",
        lastName: "",
        adress: "",
        zipCode: "",
        city: ""
    };
    const [customerInfo, setCustomerInfo] = useState(defaultCustomer);

    function updateCustomerInfo(e:React.KeyboardEvent <HTMLInputElement>){
        let name = e.currentTarget.name;
        let value = e.currentTarget.value;

        setCustomerInfo({...customerInfo, [name]:value});        
    }

    function getPaymentMethod(e:ChangeEvent <HTMLInputElement>){
        let value = e.target.value;
        props.sendPaymentMethod(value);
    }


    function sendCustomerToParent(){
        props.sendToCheckout(customerInfo);
    }    
    

    return (
        <div id="customer-form">
            <h2>Fyll i formuläret nedan</h2>
            <label htmlFor="firstName">Förnamn:</label>
            <input type="text" name="firstName" id="firstName" onKeyUp={updateCustomerInfo}/>
            <label htmlFor="lastName">Efternamn:</label>
            <input type="text" name="lastName" id="lastName" onKeyUp={updateCustomerInfo}/>
            <label htmlFor="adress">Adress:</label>
            <input type="text" name="adress" id="adress" onKeyUp={updateCustomerInfo}/>
            <label htmlFor="zipCode">Postnummer:</label>
            <input type="text" name="zipCode" id="zipCode" onKeyUp={updateCustomerInfo}/>
            <label htmlFor="city">Stad:</label>
            <input type="text" name="city" id="city" onKeyUp={updateCustomerInfo}/>
            <div>
            <label htmlFor="paymentMethod">Betalsätt: </label>
            <ul className="pay-method">
                <li>
                    <input type="radio" name="paymentMethod" id="paypal" value="paypal" onChange={getPaymentMethod}/>
                    <label htmlFor="paypal">Paypal</label>
                </li>
                <li>
                    <input type="radio" name="paymentMethod" id="visa" value="visa" onChange={getPaymentMethod}/>
                    <label htmlFor="visa">Visa</label>
                </li>
                <li>
                    <input type="radio" name="paymentMethod" id="mastercard" value="mastercard" onChange={getPaymentMethod}/>
                    <label htmlFor="mastercard">Master card</label>
                </li>
            </ul>
        </div>
            <button onClick={sendCustomerToParent}>Gå Vidare</button>
        </div>
    )
}
