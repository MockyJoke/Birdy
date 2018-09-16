import { Album } from './album'
import { NamedType } from './named-type';

export class AlbumSet implements NamedType {
    id: string;
    title: string;
    photos: Album[];

    getTypeName(): string {
        return "AlbumSet";
    }
}