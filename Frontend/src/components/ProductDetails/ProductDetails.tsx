import React, { useEffect, useState } from 'react';  
import { IProduct } from '../../Models/IProduct';
import axios from 'axios';
import './ProductDetails.scss';
import { ITrackList } from '../../Models/ITrackList';
import { IFormat } from '../../Models/IFormat';
import { IColor } from '../../Models/IColor';
import { ICategory } from '../../Models/ICategory';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSpotify } from '@fortawesome/free-brands-svg-icons'
import { IHandleExtraInfo } from '../../Models/IHandleExtraInfo';
import { ICart } from '../../Models/ICart';
import { apiUrl } from '../../App';

interface IProductDetails{
    productId: number
    sendDetailsToParent(cartItem:ICart):void;
}


export default function ProductDetails(props:IProductDetails) {
    const defaultProductList:IProduct = {
        id: 0,
        artist: "",
        album:"",
        description:"",
        color:[],
        inStock:0,
        price:0,
        format:[],
        releaseDate: "",
        imgUrl:"",
        spotifyLink:"",
        trackList:[],
        genre:[]
    };
    const [product, setProduct] = useState(defaultProductList);
    const [toggleExtras, setToggleExtras] = useState(false);
    const defaultExtraInfo:IHandleExtraInfo = {
        productColor:{productId:0, colorId: 0, colorName:""},
        productFormat:{productId:0, formatId: 0, formatName:""}
    };
    const [extraInfo, setExtraInfo] = useState(defaultExtraInfo);

    useEffect(() => {
        getProductById(localStorage.productId);
    }, []);

    async function getProductById(id:number) {
        let result = 
        await axios.get<IProduct>(`${apiUrl}/product/${id}`);
        let updateProduct:IProduct = result.data;
        
        updateProduct.trackList.sort(function(a,b){return a.trackNumber - b.trackNumber});

        setProduct(updateProduct);
    }

    function updateChoice(e: React.MouseEvent <HTMLOptionElement>){
        let name = e.currentTarget.dataset.name;
        let value = e.currentTarget.value;
        let id = e.currentTarget.dataset.id;
        let idToInt = parseInt(id!);

        if(name === "color" && id !== undefined){
            setExtraInfo({...extraInfo, productColor:{productId: product.id, colorId: idToInt, colorName: value}});
        }else if(name === "format" && id !== undefined){
            setExtraInfo({...extraInfo, productFormat:{productId: product.id, formatId: idToInt, formatName: value}});
        }        
    }

    function addToCart(){       
        let cartItemToAdd:ICart = {
            productId:product.id,
            artist: product.artist,
            album: product.album,
            amount: 1,
            colorId: extraInfo.productColor.colorId,
            colorName: extraInfo.productColor.colorName,
            formatId: extraInfo.productFormat.formatId,
            formatName: extraInfo.productFormat.formatName,
            price: product.price,
            imgUrl: product.imgUrl
        }

        if (extraInfo.productColor.colorId === 0) {
            cartItemToAdd.colorId = product.color[0].id;
            cartItemToAdd.colorName = product.color[0].name;

        }
        if (extraInfo.productFormat.formatId === 0) {            
            cartItemToAdd.formatId = product.format[0].id;
            cartItemToAdd.formatName = product.format[0].name;
        }     
        props.sendDetailsToParent(cartItemToAdd);
    }
    
    
    let formatHtml = product.format.map((f:IFormat) => {
        return(
            <p>{f.name}</p>
            )
        });
    
    let colorHtml = product.color.map((c: IColor) => {
        return(
            <p>{c.name}</p>
            )
        });
        
    let genreHtml = product.genre.map((genre: ICategory) => {
        return (
            <span>{genre.genreName}, </span>
            )
        });

    let trackListHtml = product.trackList.map((song: ITrackList) => {
        return(
            <li key={song.id}>
                <span>{song.trackNumber}.</span> <span>{song.songTitle}</span>
            </li>
        )
    });

    let colorOptionHtml = product.color.map((c:IColor) => {
        return (
            <option key={c.id} data-id={c.id} data-name="color" value={c.name} onClick={updateChoice}>{c.name}</option>
        )
    });
    let formatOptionHtml = product.format.map((f:IFormat) => {
        return (
            <option key={f.id} data-id={f.id} data-name="format" value={f.name} onClick={updateChoice}>{f.name}</option>
        )
    });

    let selectChoicesHtml = 
    <div className="extra-info">
    <label htmlFor="color">Välj Färg: </label>
    <select name="color" id="" >
        {colorOptionHtml}
    </select><br />
    <label htmlFor="format">Välj Format: </label>
    <select name="format" id="">
        {formatOptionHtml}
    </select> <br />
    <button onClick={addToCart}>Lägg i varukorg</button>
</div>



    return (
        <div key={product.id} id="product-card">
                <img src={product.imgUrl} alt=""/>
                <h2>{product.artist}</h2>
                <h3>{product.album}</h3>
                <p>{product.price}:-</p>
                <button onClick={() => setToggleExtras(!toggleExtras)}>Anpassa val</button>
                {toggleExtras ?
                selectChoicesHtml :
                <React.Fragment />}
                <p>Lagerstatus: <span>{product.inStock} st</span></p>
                <p>Releasedag: {product.releaseDate.substr(0,10)}</p>
                <div id="product-description">
                    <section id="left-section">
                        <h3>Låtlista:</h3>
                        <ul>
                            {trackListHtml}
                        </ul>
                    </section>
                <section id="right-section">
                    <h3>Beskrivning:</h3>
                    <p>{product.description}</p>
                    <div>
                        <section>
                        <h4>Format</h4>
                        {formatHtml}
                        </section>
                        <section>
                            <h4>Färger</h4>
                            {colorHtml}
                        </section>
                    </div>
                    <section>
                        <section>
                            <h4>Genre:</h4>
                            {genreHtml}
                        </section>
                        <section>
                            <h4>Streama denna platta:</h4>
                            <a href={product.spotifyLink} className="spotify-link">
                                <FontAwesomeIcon icon={faSpotify}></FontAwesomeIcon>
                            </a>
                        </section>
                    </section>
                </section>
                </div>
        </div>
    )
}
