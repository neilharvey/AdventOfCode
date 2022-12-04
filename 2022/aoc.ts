import { readFileSync } from 'fs';
import path from 'path'

export function readInput() {
    const scriptDirectory = path.dirname(process.argv[1]);
    const fileName = path.resolve(scriptDirectory, process.argv[2])
    const file = readFileSync(fileName, 'utf-8');
    return file.split("\r\n");
}