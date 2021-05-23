import React, { useState } from 'react';
import './App.scss';
import {
  BrowserRouter as Router,
  Switch,
  Route,
} from "react-router-dom";
import Navbar from './components/Navbar/Navbar';
import Categories from './components/Categories/Categories';
import Products from './components/Products/Products';
import Cart from './components/Cart/Cart';
import ProductDetails from './components/ProductDetails/ProductDetails';
import { ICart } from './Models/ICart';
import Admin from './components/Admin/Admin';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import AdminProduct from './components/AdminProduct/AdminProduct';

export const apiUrl = "https://localhost:5001/api";

function App() {
 library.add(fas);
 const defaultCart:ICart[] = [];
 const [productId, setProductId] = useState(0);
 const [userCart, setUserCart] = useState(defaultCart);

 function fetchProductId(id:number){
  setProductId(id);
 }

 function addItemToCart(newItem:ICart){
    let tempCart:ICart[] = userCart.map((item: ICart) => {
      return item;
    });

    tempCart.forEach((item: ICart, i:number) => {
      //Checks if product has same productID/colorID/formatID
      if(item.productId === newItem.productId &&
        item.colorId === newItem.colorId &&
        item.formatId === newItem.formatId)
      {
        newItem.amount += tempCart[i].amount;
        newItem.price = newItem.price * tempCart[i].amount;
        tempCart.splice(i, 1);
      }
    });
    setUserCart([...tempCart, newItem]);
 }

  return (
    <Router>
      <header>
        <Navbar updateCartCount={userCart.length}/>
      </header>
      <main>
        <aside>
          <Categories/>
        </aside>
        <Switch>
          <Route path="/" exact={true}>
            <Products
            sendIdToParent={fetchProductId}/>
          </Route>
          <Route path="/cart">
            <Cart Products={userCart}/>
          </Route>
          <Route path='/product/'>
            <ProductDetails
            productId={productId}
            sendDetailsToParent={addItemToCart}/>
          </Route>
          <Route to="/admin">
            <Admin/>
          </Route>
          <Route path="*">Does not exist</Route>
        </Switch>
      </main>
    </Router>
  );
}

export default App;
