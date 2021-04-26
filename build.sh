#! /usr/bin/env bash

# sudo apt install pandoc ttf-dejavu-extra librsvg2-bin texlive-full

source=book
fullSource=book/FullSource.md

if [ "$1" = clean ]
then
  [ -f $fullSource ] && rm $fullSource
  find . -type d -name bin -exec rm -rfv {} \;
  find . -type d -name obj -exec rm -rfv {} \;
  exit 0
fi

out=docs/MiniSpec_HowToAuthorATestingFramework_byRebeccaTaylor.pdf

latexTemplate=eisvogel
latexPreamble=templates/pdf/preamble.latex
pandocTemplateDir="$HOME/.pandoc/templates"
pandocTemplateFile="$HOME/.pandoc/templates/$latexTemplate.latex"

websiteFileHeader=templates/web/contentHeader.md
websiteSourceFile=docs/_pages/docs.md

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

echo "Compiling website"
cat $header > $fullSource
echo >> $fullSource
for chapter in $source/*
do
  [[ "$chapter" = *FullSource* ]] && continue
  [ -f "$chapter" ] || continue
  echo "$chapter --> $fullSource"
  cat "$chapter" >> $fullSource
  echo >> $fullSource
  echo >> $fullSource
done

cp $websiteFileHeader $websiteSourceFile
echo >> $websiteSourceFile
cat $fullSource | sed 's|\\frontmatter||g' | sed 's|\\mainmatter||g' | sed 's|\\pagebreak||g' >> $websiteSourceFile

[ "$1" = web ] && (( $# == 1 )) && exit 0

mkdir -p "$pandocTemplateDir"
echo "Updating $pandocLatexFile --> $pandocTemplateFile"
cp $pandocLatexFile "$pandocTemplateFile"

echo "Generating pdf..."
pandoc                         \
  --toc                        \
  --toc-depth=3                \
  --listings                   \
  --template $latexTemplate    \
  --highlight-style pygments   \
  --top-level-division=chapter \
  --pdf-engine xelatex         \
  -H "$latexPreamble"          \
  -V classoption=oneside       \
  -V linkcolor:blue            \
  -f markdown+raw_tex          \
  -o "$out"                    \
  "$fullSource"

echo "Generated $out"
echo
echo "Done."

  # -V mainfont="DejaVu Serif"     \
  # -V monofont="DejaVu Sans Mono" \