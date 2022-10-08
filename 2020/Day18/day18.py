import sys
import math

def evaluate_expression(tokens, advanced):

    # evaluate all the brackets first
    brackets = []
    for (index,token) in enumerate(tokens):
        if token == '(':
            brackets.append(index)
        elif token == ')':
            bracket_start = brackets.pop()
            new_tokens = tokens[0:bracket_start] + [evaluate_expression(tokens[bracket_start+1:index], advanced)] + tokens[1+index:len(tokens)]
            return evaluate_expression(new_tokens, advanced)

    if not advanced:
        # Part One: 
        # Operators have the same precedence, and are evaluated left-to-right
        index = 0
        while index < len(tokens):
            token = tokens[index]
            if token == '+' or token == '*':
                l = tokens.pop(index - 1)
                r = tokens.pop(index)
                if token == '+':
                    tokens[index - 1] = l + r
                else:
                    tokens[index - 1] = l * r
                index -= 2              
            index += 1
        return tokens[0]

    else:
        # Part Two:
        # Addition is evaluated before multiplication.
        index = 0
        while index < len(tokens):
            token = tokens[index]
            if token == '+':
                l = tokens.pop(index - 1)
                r = tokens.pop(index)
                tokens[index - 1] = l + r
                index -= 2
            index += 1

        return math.prod([int(n) for n in tokens if n != '*'])

def main():

    path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
    with open(path) as f:
        homework = f.read().splitlines()

    basic_sum = 0
    advanced_sum = 0
    for expression in homework:
        tokens = []
        for char in expression:
            if char.isnumeric():
                tokens.append(int(char))
            elif not char.isspace():
                tokens.append(char)

        basic_sum += evaluate_expression(tokens, False)
        advanced_sum += evaluate_expression(tokens, True)
        
    print(f"Part One: {basic_sum}")    
    print(f"Part Two: {advanced_sum}")

if __name__ == "__main__":
    main()