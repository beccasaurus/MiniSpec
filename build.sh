#! /usr/bin/env bash

# sudo apt install pandoc ttf-dejavu-extra librsvg2-bin texlive-full

source=book/content.md
fullSource=book/full.md

out=docs/MiniSpec_HowToAuthorATestingFramework_byRebeccaTaylor.pdf

latexTemplate=eisvogel
latexPreamble=templates/pdf/preamble.latex
pandocTemplateDir="$HOME/.pandoc/templates"
pandocTemplateFile="$HOME/.pandoc/templates/$latexTemplate.latex"

if [ "$1" = --full ]
then
  header=templates/pdf/FullSourceHeader.md
  pandocLatexFile=templates/pdf/$latexTemplate-cover-image.latex
  shift
elif [ "$1" = --cover ]
then
  header=templates/pdf/MediumSourceHeader.md
  pandocLatexFile=templates/pdf/$latexTemplate-cover-image.latex
  shift
else
  header=templates/pdf/FullSourceHeaderNoFrontBack.md
  pandocLatexFile=templates/pdf/$latexTemplate-cover-text.latex
fi

echo "Compiling $source --> $fullSource"
cp $header $fullSource
echo >> $fullSource
cat $source >> $fullSource

mkdir -p "$pandocTemplateDir"
echo "Updating $pandocLatexFile --> $pandocTemplateFile"
cp $pandocLatexFile "$pandocTemplateFile"

echo "Generating pdf..."
pandoc                         \
  --toc                        \
  --toc-depth=3                \
  --template $latexTemplate    \
  --highlight-style pygments   \
  -H "$latexPreamble"          \
  -V classoption=oneside       \
  -f markdown+raw_tex          \
  -o "$out"                    \
  "$fullSource"

echo "Generated $out"
echo
echo "Done."
  # --top-level-division=chapter \