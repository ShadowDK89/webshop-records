import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { Link, Route } from 'react-router-dom';
import { IAdminOrder, IAdminOrderProducts } from '../../Models/IOrder';
import AdminOrder from '../AdminOrder/AdminOrder';
import AdminProduct from '../AdminProduct/AdminProduct';
import './Admin.scss'

export default function Admin() {
    const [showAdminProduct, setShowAdminProduct] = useState(false);


    
    return (
        <div id="admin-container">
            <div id="admin-nav">
                <Link to="/admin">
                    <button onClick={() => setShowAdminProduct(false)}>Orders List</button>
                </Link>
                <Link to="/admin/product">
                <button onClick={() => setShowAdminProduct(true)}>Add New Product</button>
                </Link>
            </div>
            <Route to="/admin">
                {!showAdminProduct ? <AdminOrder></AdminOrder> : <React.Fragment></React.Fragment>}
            </Route>
            <Route to="/admin/product">
                {showAdminProduct ? <AdminProduct></AdminProduct> : <React.Fragment></React.Fragment>}
            </Route>
        </div>
    )
}
