import { Album } from './album'
import { NamedType } from './named-type';

export class AlbumSet implements NamedType {

    public id: string;
    public name: string;
    private albums: Album[];

    constructor(id: string, name: string) {
        this.id = id;
        this.name = name;
    }

    getAlbums(): Album[] {
        return this.albums;
    }

    getTypeName(): string {
        return "AlbumSet";
    }

    setAlbums(albums: Album[]){
        this.albums = albums;
    }
}