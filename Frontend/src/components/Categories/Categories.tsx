import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { ICategory } from '../../Models/ICategory'
import './Categories.scss'
import { apiUrl } from '../../App';

export default function Categories() {
    const defaultCategory:ICategory[] = [];
    const [categoryList, setCategoryList] = useState(defaultCategory);

    useEffect(() => {
        getCategories();
    }, []);

    async function getCategories() {
        try {
            let result =
            await axios.get<ICategory[]>(`${apiUrl}/genres/`);
            let updateCategories:ICategory[] = result.data.map((category:ICategory) => {
                return category;
            });           
           setCategoryList([...updateCategories]);       
            
        } catch (error) {
            
        }
    }

    let categoriesHtml = categoryList.map((category:ICategory) => {
        return(
        <li key={category.id} data-category-id={category.id}>
            <a href="#">{category.genreName}</a>
        </li>
        );
    })


    return(<ul>
        {categoriesHtml}
    </ul>)
}
