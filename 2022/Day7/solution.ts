import { readLines } from '../aoc';

class Directory {

    label: string
    parent: Directory | undefined;
    size: number;
    directories: Map<string, Directory>;

    constructor(label: string, parent?: Directory) {
        this.label = label;
        this.parent = parent;
        this.size = 0;
        this.directories = new Map<string, Directory>();
    }

    addFile(size: number) {
        this.size += size;
        this.parent?.addFile(size);
    }
}

function change_directory(root: Directory, cwd: Directory, dir: string) {
    if (dir == "/") {
        return root;
    }

    if (dir == "..") {
        if (cwd.parent === undefined) {
            throw "Unable to return parent of root directory";
        }

        return cwd.parent;
    }

    if (!cwd.directories.has(dir)) {
        cwd.directories.set(dir, new Directory(dir, cwd));
    }

    return cwd.directories.get(dir)!;
}

function getDirectoryStructure(terminal: string[]) {

    const root = new Directory("/");
    let cwd = root;

    terminal.forEach(line => {
        if (line.startsWith("$ cd")) {
            let dir = line.substring(5);
            cwd = change_directory(root, cwd, dir);
        } else if (!line.startsWith("$")) {
            let size = line.substring(0, line.indexOf(" "));
            let name = line.substring(1 + line.indexOf(" "));
            if (size != 'dir') {
                cwd.addFile(Number(size));
            }
        }
    });

    return root;
}

function getDirectorySizes(root: Directory) {

    let directorySizes: number[] = []
    directorySizes.push(root.size);
    root.directories.forEach(x => getDirectorySizes(x).forEach(value => directorySizes.push(value)));
    return directorySizes;
}

const terminal = readLines();
const root = getDirectoryStructure(terminal);
const directorySizes = getDirectorySizes(root);
const smallDirectoriesSize = directorySizes.filter(size => size <= 100000).reduce((a, c) => a + c);
console.log(`Part One: ${smallDirectoriesSize}`);

const requiredSpace = 30000000 - (70000000 - root.size);
const spaceToDelete = directorySizes.filter(size => size >= requiredSpace).reduce((x, y) => { return (x < y) ? x : y });
console.log(`Part Two: ${spaceToDelete}`);