import { ICategory } from "./ICategory";
import { IColor } from "./IColor";
import { IFormat } from "./IFormat";
import { ITrackList } from "./ITrackList";

export interface IProduct{
    id: number,
    artist: string,
    album: string,
    description: string,
    color: IColor[],
    inStock: number,
    price: number,
    format: IFormat[],
    releaseDate: string,
    imgUrl: string,
    spotifyLink: string,
    trackList: ITrackList[]
    genre: ICategory[]
}