import { Photo } from './photo'
import { NamedType } from './named-type';

export class Album implements NamedType {
    id: string;
    title: string;
    photos: Photo[];

    getTypeName(): string {
        return "Album";
    }
}