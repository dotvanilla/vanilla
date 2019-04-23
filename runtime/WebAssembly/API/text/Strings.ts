namespace WebAssembly {

    /**
     * A module contains string related api for simulate 
     * ``Microsoft.VisualBasic.Strings`` module.
    */
    export module Strings {

        export function Mid(text: number, from: number, length: number): number {
            let string: string = ObjectManager.readText(text);
            let substr: string = string.substr(from - 1, length);

            return ObjectManager.addText(substr);
        }

        export function Len(text: number): number {
            return ObjectManager.readText(text).length;
        }

        export function UCase(text: number): number {
            let string: string = ObjectManager.readText(text);

            if (string) {
                string = string.toUpperCase();
            } else {
                string = "";
            }

            return ObjectManager.addText(string);
        }

        export function LCase(text: number): number {
            let string: string = ObjectManager.readText(text);

            if (string) {
                string = string.toLowerCase();
            } else {
                string = "";
            }

            return ObjectManager.addText(string);
        }
    }
}