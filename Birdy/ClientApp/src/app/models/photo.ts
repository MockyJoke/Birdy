import { NamedType } from "./named-type"

export class Photo implements NamedType {
    albumSetId: string;
    albumId: string;
    id: string;
    name: string;

    constructor(albumSetId: string, albumId: string, id: string, name: string) {
        this.albumSetId = albumSetId;
        this.albumId = albumId;
        this.id = id;
        this.name = name;
    }
    getTypeName(): string {
        return "Photo";
    }
}