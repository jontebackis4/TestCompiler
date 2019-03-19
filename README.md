# _ZIFRO_ Mellis compiler

[![CircleCI branch: rework](https://img.shields.io/circleci/token/32eed7ac8019c96700eeb31e7feacc00b57d3899/project/github/zardan/compiler/rework.svg?label=%2Frework&logo=circleci)](https://circleci.com/gh/zardan/compiler)
[![CircleCI branch: all](https://img.shields.io/circleci/token/32eed7ac8019c96700eeb31e7feacc00b57d3899/project/github/zardan/compiler.svg?label=%2Fall&logo=circleci)](https://circleci.com/gh/zardan/compiler)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/ce4b06e6b3c243d29cb25ed8462980ae?branch=rework)](https://www.codacy.com?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=zardan/compiler&amp;utm_campaign=Badge_Grade)

Mellis is a code library to allow compilation and code-walking of scripting languages such as Python.
Built using .NET Standard 2.0, and for that is easily integrated into any .NET environment.

The main use case of Mellis is for educational applications to allow users to write code and see it execute in a controlled environment.
We at Zifro are using Mellis in our [Zifro Playground](https://www.zifro.se/#playground).

Mellis is developed, maintained, and owned by © Zifro AB ([zifro.se](https://zifro.se/))

## Key features:

- Runtime compilation! The scripting language is compiled and executed in runtime.
- Code walking! Step through the compiled script line-by-line!
- Simple pedagogical localized error messages. _(Ex: `You forgot to add a colon ":" at the end of your if statement.`, instead of `syntax error in C:\Users\me\Documents\My Python scripts\example.py:3: unexpected EOF while parsing`)_
- Unified language interface. Easily open up code modules to any (supported) programming language.
- No threads. So easy integration into unthreaded environments such as WebAssembly _[(soon not relevant though, kudos to the webasm team 🤞)](https://github.com/WebAssembly/threads)_.

## Probable additions:

- Languages
  - Lua
  - JavaScript

## Star wishes, low prio, additions:

- Languages
  - C#
  - Go
  - Perl
