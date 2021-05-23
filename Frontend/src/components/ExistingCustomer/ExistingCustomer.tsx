import axios from 'axios';
import React, { ChangeEvent, useEffect, useState } from 'react'
import { apiUrl } from '../../App';
import { ICustomer } from '../../Models/ICustomer';
import './ExistingCustomer.scss';

interface IExistionCustomerProps{
    updateSelectedCustomer(customer:ICustomer):void;
    sendPaymentMethod(paymentMethod: string):void;
    proceedWithOrder():void;
}

export default function ExistingCustomer(props: IExistionCustomerProps) {
    const defaultCustomers:ICustomer[] = [];
    const [customerList, setCustomerList] = useState(defaultCustomers);
    const defaultSingleCustomer:ICustomer = {
        id: 0,
        firstName: "",
        lastName: "",
        adress: "",
        zipCode: "",
        city: ""
    };

    const [selectedCustomer, setSelectedCustomer] = useState(defaultSingleCustomer);

    useEffect(() => {
        getCustomers();    
    }, []);


    async function getCustomers(){
        let result = 
        await axios.get<ICustomer[]>(`${apiUrl}/customer/`);
        let updateCustomerList = result.data.map((customer: ICustomer) => {
            return customer
        });
        setCustomerList([...updateCustomerList]);
    }

    function selectCustomer(e:React.SyntheticEvent <HTMLOptionElement> ){
        let idToMatch = e.currentTarget.dataset.id;

        customerList.forEach((customer:ICustomer) => {
            if(customer.id === parseInt(idToMatch!)){
                setSelectedCustomer(customer);
                props.updateSelectedCustomer(customer);
            }
        });        
    }

    function getPaymentMethod(e:ChangeEvent <HTMLInputElement>){
        let value = e.target.value;        
        props.sendPaymentMethod(value);
    }

    function proceedToCheckoutFrom(){
        props.proceedWithOrder();
    }

    let customerListHtml = customerList.map((customer: ICustomer) => {
        return(
            <option key={customer.id} onClick={selectCustomer} data-id={customer.id}>{customer.firstName} {customer.lastName}</option>
        );
    });


    let customerSelectHtml =
    <div id="existing-customer-container">
        <select name="customers" id="">
            {customerListHtml}
        </select>
        <div className="payment-container">
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
        <button onClick={proceedToCheckoutFrom}>Nästa</button>
    </div> 



    return (
        <div>
            {customerSelectHtml}
        </div>
    )
}
