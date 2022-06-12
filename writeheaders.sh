for x in $(find . | grep '\.fs$' | grep -v '/obj/')
do
    z=$(
    cat << EOF 
// --------------------------------------------------
// $x
// --------------------------------------------------
// 
$(git whatchanged $x | grep 'Author\|Date' | sed -e 's/<.*>//' | sed -e 's/2022 +0200/\n/' | sed -e 's/^/\/\/ /' | head -n -1)
EOF
)
    echo "${z}" >> $x


done

