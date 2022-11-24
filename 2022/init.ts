import fs from 'fs';
import path from 'path'

const directory = path.resolve(__dirname, process.argv[2])
const template = path.resolve(__dirname, "Template");

if(fs.existsSync(directory))
{
    console.error(`Failed: Solution directory ${directory} already exists.`)
}
else
{
    fs.cpSync(template, directory, { recursive: true });
}