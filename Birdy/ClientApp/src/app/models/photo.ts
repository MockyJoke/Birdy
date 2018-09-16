import { NamedType } from "./named-type"

export class Photo implements NamedType {
    url: string;
    id: string;

    getTypeName(): string {
        return "Photo";
    }
}