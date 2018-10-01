import { Album } from './album'
import { NamedType } from './named-type';

export class AlbumSet implements NamedType {
    
    public id: string;
    public name: string;

    constructor(id: string, name: string){
        this.id = id;
        this.name = name;
    }

    getTypeName(): string {
        return "AlbumSet";
    }
}