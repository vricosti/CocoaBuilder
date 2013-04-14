###############TRICK SCRIPT############
#
# To do the work of cscc , and work with
# pinvoke from a specific lib $PINVOKE_SO
# for funcs in $PINVOKE_FUNCS
#######################################

PINVOKE_FILE="Native.cs"
PINVOKE_PREFIX="_xml"
PINVOKE_FUNCS=`cat $PINVOKE_FILE | grep "$PINVOKE_PREFIX" | sed "s/.*\($PINVOKE_PREFIX[^(]*\).*$/\1/"`
PINVOKE_SO="libxml_wrapper"
TMP0_FILE="$1.iltmp0"
TMP1_FILE="$1.iltmp1"
SWAP_VAR=""
OUT_FILE="$1"

[ -f $1 ] || echo "Not found $1"
[ -f $1 ] || exit 0;


grep "pinvokeimpl" $1 >/dev/null && echo "already processed ?";
grep "pinvokeimpl" $1 >/dev/null && exit 0;

cp $1 $1~
cat $1 > $TMP0_FILE


for each in $PINVOKE_FUNCS;
	do 
	cat $TMP0_FILE | sed "s/\(\.method.*hidebysig\)\(.*\)'$each'(/\1 pinvokeimpl(\"$PINVOKE_SO\")\2'$each'(/g" > $TMP1_FILE; 
	if [ `python -c "print 1" >> /dev/null 2>&1 && echo 1` ] ; then
		DOTS=`python -c "i=len(\"$each\");print \".\"*(50-i),"` 
	fi
	echo "processing " $each $DOTS 
	SWAP_VAR=$TMP1_FILE;	
	TMP1_FILE=$TMP0_FILE;
	TMP0_FILE=$SWAP_VAR;
	done

cat $TMP0_FILE > $OUT_FILE
rm -f $TMP1_FILE $TMP0_FILE
