import React, { useEffect, useState } from 'react'
import './Navbar.scss'
import { faShoppingCart } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import Logo from '../../assets/img/logo'
import { Link } from 'react-router-dom'

interface INavbarProps{
    updateCartCount:number;
}

export default function Navbar(props:INavbarProps) {
    const [cartCount, setCartCount] = useState(0);
    useEffect(() => {     
        updateCount();
    }, [props.updateCartCount])

    function updateCount(){
        setCartCount(props.updateCartCount);
    }

    return (
            <nav>
                <Link to='/'>
                    <div id="logotype">
                        <Logo/>
                        <span>Shiny Records</span>
                    </div>
                </Link>
                <div>
                    <input type="text" name="" id=""/>
                    <button>Sök</button>
                </div>
                <Link to='/cart' id="cart-button">
                    <span id="cart-icon">
                        <FontAwesomeIcon icon={faShoppingCart}></FontAwesomeIcon>
                        {props.updateCartCount > 0 ? <span id="cart-counter">{cartCount}</span>
                        : <React.Fragment></React.Fragment>}
                    </span>
                </Link>
            </nav>
    )
}
