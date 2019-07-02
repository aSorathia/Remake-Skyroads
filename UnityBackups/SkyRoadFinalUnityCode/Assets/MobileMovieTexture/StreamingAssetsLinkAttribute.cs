using UnityEngine;
using System;
using System.Collections;

namespace MMT
{
	public class StreamingAssetsLinkAttribute : PropertyAttribute
	{
	    public Type LinkType { get;  set; }
	    public string Label { get;  set; }

	    public StreamingAssetsLinkAttribute(Type a_type, string a_label)
	    {
	        this.LinkType = a_type;
			this.Label = a_label;
	    }
	}
}