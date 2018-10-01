import { Photo } from './photo'
import { NamedType } from './named-type';

export class Album implements NamedType {
    albumSetId: string;
    id: string;
    name: string;

    constructor(albumSetId: string, id: string, name: string) {
        this.albumSetId = albumSetId;
        this.id = id;
        this.name = name;
    }

    getTypeName(): string {
        return "Album";
    }
}