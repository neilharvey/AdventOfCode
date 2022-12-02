import { readFileSync } from 'fs';
import path from 'path'
const fileName = path.resolve(__dirname, process.argv[2])
const file = readFileSync(fileName, 'utf-8');
const lines = file.split("\r\n");

console.log(lines);