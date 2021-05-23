import React, { useEffect, useState } from 'react'
import axios from 'axios'
import { IProduct } from '../../Models/IProduct'
import './Products.scss'
import { Link } from 'react-router-dom';
import { apiUrl } from '../../App';

interface IProductProps{
    sendIdToParent(id:number):void;
}


export default function Products(props:IProductProps) {
    const defaultProductList:IProduct[] = [];
    const [productList, setProductList] = useState(defaultProductList);

    useEffect(() => {
        getProducts();
    }, []);

    async function getProducts(){
        try {
            let result = 
            await axios.get<IProduct[]>(`${apiUrl}/product/`);
    
            let updateProductList = result.data.map((product:IProduct) => {
                return product; 
            });       
            setProductList([...updateProductList]);
            
        } catch (error) {
            
        }
    }

    function fetchProductId(e:React.MouseEvent <HTMLAnchorElement>){
        let getId = e.currentTarget.dataset.productId;
        localStorage.productId = getId;
        let idToInt = parseInt(getId!);
        props.sendIdToParent(idToInt);
    }


    let productListHtml = productList.map((product:IProduct) => {
        return(
            <div className="product-card" key={product.id}>
                <Link to={`product/${product.id}`} data-product-id={product.id} onClick={fetchProductId}><img src={product.imgUrl} alt=""/></Link>
                <h3>{product.artist}</h3>
                <p>{product.album}</p>
                    <p className="product-price">{product.price}:-</p>
                    <Link to={`product/${product.id}`} data-product-id={product.id} onClick={fetchProductId}>
                        <button data-product-id={product.id}>Visa Produkt</button>
                    </Link>
                    
            </div>
        )
    });

    return (
        <div id="product-container">
            <h1 id="product-list-heading">Alla produkter</h1>
            <div id="product-list">
                {productListHtml}
            </div>
        </div>
    )
}
