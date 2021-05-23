import axios from 'axios';
import React, { ChangeEvent, MouseEvent, useEffect, useState } from 'react'
import { JsxEmit } from 'typescript';
import { apiUrl } from '../../App';
import { ICategory } from '../../Models/ICategory';
import { IColor } from '../../Models/IColor';
import { IFormat } from '../../Models/IFormat';
import { IProduct } from '../../Models/IProduct';
import { ITrackList } from '../../Models/ITrackList';
import './AdminProduct.scss'

export default function AdminProduct() {
    const defaultProduct:IProduct = {
        id:0,
        artist:"",
        album:"",
        description:"",
        color: [],
        inStock:0,
        price:0,
        format:[],
        releaseDate:"",
        imgUrl:"",
        spotifyLink:"",
        trackList:[],
        genre:[]
    };
    let defaultJSX:JSX.Element[] = [];
    const defaultColorList:IColor[] = [];
    const defaultFormatList:IFormat[] = [];
    const defaultGenreList:ICategory[] = [];
    const [productToAdd, setProductToAdd] = useState(defaultProduct);
    const [colorList, setColorList] = useState(defaultColorList);
    const [formatList, setFormatList] = useState(defaultFormatList);
    const [genreList, setGenreList] = useState(defaultGenreList);
    const [addNewTrackLine, setAddNewTrackLine] = useState(defaultJSX);

    useEffect(() => {
       getColors();
       getFormats();
       getGenre();
    }, []);

    async function getColors(){
        let result = 
            await axios.get<IColor[]>(`${apiUrl}/color`);
        let updateColorList = result.data.map((c:IColor) => {
            return c;
        });
        setColorList(updateColorList);
    }
    async function getFormats(){
        let result = 
            await axios.get<IFormat[]>(`${apiUrl}/format`);
        let updateFormatList = result.data.map((c:IFormat) => {
            return c;
        });
        setFormatList(updateFormatList);
    }
    async function getGenre(){
        let result = 
            await axios.get<ICategory[]>(`${apiUrl}/genres`);
        let updateGenreList = result.data.map((c:ICategory) => {
            return c;
        });
        setGenreList(updateGenreList);
    }

    function updateNewProduct(e:ChangeEvent <HTMLInputElement>){
        let name = e.target.name;
        let value = e.target.value;

        setProductToAdd({...productToAdd, [name]: value});
    }

    function showProduct(e:MouseEvent <HTMLButtonElement>){
        e.preventDefault();
        console.log(productToAdd);
        
    }

    function generateNewLine(e:MouseEvent <HTMLButtonElement>){
        e.preventDefault();
        setAddNewTrackLine([...addNewTrackLine, tracklistHtml]);          
    }
    
    let tracklistHtml = 
    <div className="track-input">
    <label htmlFor="trackNumber">Låtnummer:</label>
    <input type="number" name="trackNumber" id="trackNumber"/>
    <label htmlFor="songTitle">Låtnamn:</label>
    <input onChange={updateNewProduct} type="text" name="songTitle" id="songTitle"/>
    <button>Lägg till</button>
    </div>;


    let colorListHtml = colorList.map((c:IColor) => {
        return(
            <React.Fragment>
                <div>
                    <input key={c.id} type="checkbox" name="color" id="color" value={c.id} />{c.name}
                    <label htmlFor={c.name}></label>
                </div>
            </React.Fragment>
        )
    });

    let formatListHtml = formatList.map((f:IFormat) => {
        return(
            <React.Fragment>
                <div>
                    <input key={f.id} type="checkbox" name="format" id="format" value={f.id}/>
                    <label htmlFor={f.name}>{f.name}</label>
                </div>
            </React.Fragment>
        )
    });

    let genreListHtml = genreList.map((g:ICategory) => {
        return(
            <div>
                <input key={g.id} type="checkbox" name="genre" id="genre" value={g.id}/>
                <label htmlFor={g.genreName}>{g.genreName}</label>
            </div>
        )
    });

    return (
        <div id="admin-product-container">
            <form id="admin-product-form">
                <label htmlFor="artist">Artist</label>
                <input onChange={updateNewProduct} type="text" name="artist" id="artist"/>
                <label htmlFor="album">Album</label>
                <input onChange={updateNewProduct} type="text" name="album" id="album"/>
                <div id="admin-form-color">
                    <label htmlFor="color">Färg på Skivan</label>
                    <div id="color-container">
                        {colorListHtml}
                    </div>
                </div>
                <div id="admin-form-format">
                    <label htmlFor="format">Vilka format finns tillgängligt?</label>
                    {formatListHtml}
                </div>
                <div id="admin-form-genre">
                    <label htmlFor="genre">Vilka genrer ingår?</label>
                    <div id="genre-container">
                        {genreListHtml}
                    </div>
                </div>
                <label htmlFor="price">Pris</label>
                <input type="number" name="price" id="price"/>
                <label htmlFor="description">Beskrivning</label>
                <textarea name="description" id="description"
                rows={5} cols={10} placeholder="Kort beskrivning av albumet" maxLength={50}>
                </textarea>
                <div id="track-container">
                <button onClick={generateNewLine}>Ny låt</button>
                    {tracklistHtml}
                    {addNewTrackLine}
                </div>
                <label htmlFor="inStock">Lager Status</label>
                <input onChange={updateNewProduct} type="number" name="inStock" id="inStock"/>
                <label htmlFor="releaseDate">Release dag</label>
                <input onChange={updateNewProduct} type="date" name="releaseDate" id="releaseDate"/>
                <label htmlFor="imgUrl">URL till Album Cover</label>
                <input onChange={updateNewProduct} type="text" name="imgUrl" id="imgUrl"/>
                <label htmlFor="spotifyLink">URL till Spotify</label>
                <input onChange={updateNewProduct} type="text" name="spotifyLink" id="spotifyLink"/>
                <button onClick={showProduct}>Lägg till</button>
            </form>
        </div>
    )
}
