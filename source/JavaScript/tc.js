var pics=new Array();
var stat=new Array();
var pic=new Array();

function changepic(i,code)
{ var loop=0;

	if (stat[code])
	{
		while ((pics[code][i]==0 || i>=16) && loop<100)
		{ if (i>=16) { i=0; }
			else
			{ i++;
			}		
			loop++;
		}
		
		if (pic[code][i].complete)
		{	document.getElementById(code).src=pic[code][i].src;
			setTimeout("changepic("+(i+1)+",'"+code+"')",500);
		}
		else
		{	setTimeout("changepic("+i+",'"+code+"')",20);
		}
	}
	
	
}

function loadpic(url,code,j)
{	if (stat[code]) { pic[code][j].src=url; }
}

function startm(code,ta,te)
{	stat[code]=1;
	var jj, jjj;
	var first=1;
	
	for(var j=0;j<16;j++)
	{ if (pics[code][j]==1)
		{ pic[code][j]=new Image();
			jj=j+1;
			if (jj>=100) { jjj=""+jj; }
			if (jj<100 && jj>=10) { jjj="0"+jj; }
			if (jj<10) { jjj="00"+jj; }
			if (first) { first=0; loadpic(ta+jjj+te,code,j); }
			else { setTimeout("loadpic('"+ta+jjj+te+"','"+code+"',"+j+")",j*50); }
		}
	}
	changepic(0,code);
}

function endm(code)
{	stat[code]=0;
}
