using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createletter : MonoBehaviour {

    [System.Serializable]
    public class OpacitySetting
    {
        public float opacity;
        public float fadein;
        public float fadeout;
        public float fadetime;
        public float startFadeAt;
    }

    [System.Serializable]
    public class WiggleSetting
    {
        public float wiggle;
        public float decrease;
        public float increase;
        public float creasetime;
        public float startWiggleAt;
    }

    [System.Serializable]
    public class PositionSetting
    {
        public GameObject InstantTextLettercontainer;
        public GameObject HoverLettercontainer;
        public GameObject ClickLettercontainer;
        public float x;
        public float y;
    }

    public PositionSetting PositionSettings;

    public string instantText;
    public string hoverText;
    public string clickText;
    //public string timeText;

    public float delay;

    public OpacitySetting OpacitySettings;
    public WiggleSetting WiggleSettings;

    public float zise;
    //public float grow;
    //public float shrink;
    //public float zisetime;

    //public float gap;

    //public float coloR;
    //public float coloG;
    //public float coloB;

    private float yoffset;
    private float xoffset;
    private float clickxoffset;
    private float clickyoffset;
    private float textyoffset;
    private float textxoffset;
    private float readytoclick = 0;

    private Coroutine textanimation;
    private Coroutine hoveranimation;
    private Coroutine clickanimation;

    void Start () {

        if (PositionSettings.HoverLettercontainer == null)
        {
            PositionSettings.HoverLettercontainer = this.gameObject;
        }

        if (PositionSettings.ClickLettercontainer == null)
        {
            PositionSettings.ClickLettercontainer = this.gameObject;
        }

        if (PositionSettings.InstantTextLettercontainer == null)
        {
            PositionSettings.InstantTextLettercontainer = this.gameObject;
        }

        textanimation = StartCoroutine(TextCoroutine());
    }
	
	void Update () {



        if (Input.GetMouseButtonDown(0) && readytoclick == 1)
        {
            clickanimation = StartCoroutine(ClickCoroutine());
            readytoclick = 4;

            StopCoroutine(hoveranimation);
            yoffset = 0;
            xoffset = 0;
            foreach (Transform child in PositionSettings.HoverLettercontainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        if (readytoclick == 2)
        {
            foreach (Transform child in PositionSettings.ClickLettercontainer.transform)
            {
                GameObject.Destroy(child.gameObject);

               
            }
                clickxoffset = 0;
                clickyoffset = 0;

                readytoclick = 5;
        }

        if (readytoclick == 5 && Input.GetMouseButtonDown(0))
        {
            foreach (Transform child in PositionSettings.ClickLettercontainer.transform)
            {
                GameObject.Destroy(child.gameObject);


            }
            clickxoffset = 0;
            clickyoffset = 0;
            readytoclick = 0;
            StopCoroutine(clickanimation);
        }
    }

    IEnumerator TextCoroutine()
    {
        for (int i = 0; i < instantText.Length; i++)
        {

            textxoffset += 0.5f;

            GameObject textletter = Instantiate(Resources.Load(instantText[i].ToString(), typeof(GameObject)), transform.position + new Vector3(textxoffset + PositionSettings.x, textyoffset + PositionSettings.y, 0), Quaternion.identity) as GameObject;

            textletter.transform.parent = PositionSettings.InstantTextLettercontainer.transform;

            if (instantText[i].ToString() == "-")
            {
                textyoffset -= 0.8f;
                textxoffset = 0;
            }

            if (delay > 0) 
            {
                 yield return new WaitForSeconds(delay);
            }
        }
        yield return new WaitForSeconds(delay);
    }

    IEnumerator HoverCoroutine()
    {
        for (int i = 0; i < hoverText.Length; i++)
        {

            xoffset += 0.5f;

            GameObject hoverletter = Instantiate(Resources.Load(hoverText[i].ToString(), typeof(GameObject)), transform.position + new Vector3(xoffset+ PositionSettings.x, yoffset+ PositionSettings.y, 0), Quaternion.identity) as GameObject;

            hoverletter.transform.parent = PositionSettings.HoverLettercontainer.transform;

            if (hoverText[i].ToString() == "-")
            {
                yoffset -= 0.8f;
                xoffset = 0;
            }

            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }
        }
        yield return new WaitForSeconds(delay);

    }

    IEnumerator ClickCoroutine()
    {
        for (int i = 0; i < clickText.Length; i++)
        {

            clickxoffset += 0.5f;

            GameObject clickletter = Instantiate(Resources.Load(clickText[i].ToString(), typeof(GameObject)), transform.position + new Vector3(clickxoffset + PositionSettings.x, clickyoffset + PositionSettings.y, 0), Quaternion.identity) as GameObject;

            clickletter.transform.parent = PositionSettings.ClickLettercontainer.transform;

            if (clickText[i].ToString() == "-")
            {
                clickyoffset -= 0.8f;
                clickxoffset = 0;
            }

            while (clickText[i].ToString() == "#")
            {
                yield return null;
                readytoclick = 3;

                if (Input.GetMouseButtonDown(0) && readytoclick == 3)
                {
                    i++;
                    readytoclick = 2;
                }
            }

                yield return new WaitForSeconds(delay);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "zeiger")
        {

            if (readytoclick == 0)
            {
                hoveranimation = StartCoroutine(HoverCoroutine());
                readytoclick = 1;
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        yoffset = 0;
        xoffset = 0;

        if (hoveranimation != null)
        {
            StopCoroutine(hoveranimation);
        }


        if (readytoclick == 1)
        {
            readytoclick = 0;
        }

        if (readytoclick == 0)
        {
            foreach (Transform child in PositionSettings.HoverLettercontainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
