using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letters : MonoBehaviour {

    public GameObject parent;
    private Createletter reference;

    private float miggle;
    private Transform around;
    Vector3 start;

    private float sise;

    private float obacity;
    private float veout;
    private float vedein;
    private float vedetime;
    private float startfadetime;
    private float startvadetime;

    private float kreasetime;
    private float kreasetimestart;
    private float tecrease;
    private float jnecrease;
    private float startcreasetime;

    private Color c;

    public GameObject target;

    void Start () {

        parent = transform.parent.gameObject;
        reference = parent.GetComponent<Createletter>();

        around = GetComponent(typeof(Transform)) as Transform;
        start = around.position;

        sise = reference.zise;

        obacity = reference.OpacitySettings.opacity;
        veout = reference.OpacitySettings.fadeout;
        vedein = reference.OpacitySettings.fadein;
        vedetime = reference.OpacitySettings.fadetime;
        startfadetime = reference.OpacitySettings.fadetime;
        startvadetime = reference.OpacitySettings.startFadeAt;

        miggle = reference.WiggleSettings.wiggle;
        tecrease = reference.WiggleSettings.decrease;
        jnecrease = reference.WiggleSettings.increase;
        kreasetime = reference.WiggleSettings.creasetime;
        kreasetimestart = reference.WiggleSettings.creasetime;
        startcreasetime = reference.WiggleSettings.startWiggleAt;

        transform.localScale += new Vector3(sise, sise, 0);
        c = GetComponent<SpriteRenderer>().color;
        c.a = obacity;
        GetComponent<SpriteRenderer>().color = c;

        GetComponent<letters>().target = GameObject.Find("zeiger");
    }

	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 100 * Time.deltaTime);

        if (startvadetime > 0)
        {
            startvadetime -= 1;
        }

        if (startvadetime <= 0)
        {
            if (startfadetime <= 0 && veout >= 0)
            {
                vedetime = 1;
                startfadetime = 1;
            }

            if (obacity >= 0 && vedetime >= 1 && veout >= 0)
            {
                obacity -= (veout / startfadetime) * 2;
                vedetime -= 1;
            }


            if (startfadetime <= 0 && vedein >= 0)
            {
                vedetime = 1;
                startfadetime = 1;
            }

            if (obacity >= 0 && vedetime >= 1 && vedein >= 0)
            {
                obacity += (vedein / startfadetime) * 2;
                vedetime -= 1;
            }

            c.a = obacity;
            GetComponent<SpriteRenderer>().color = c;
        }


        if (startcreasetime > 0)
        {
            startcreasetime -= 1;
        }

        if (startcreasetime <= 0)
        {

            if (kreasetimestart <= 0 && tecrease >= 0)
            {
                kreasetime = 1;
                kreasetimestart = 1;
            }

            if (miggle >= 0 && kreasetime >= 1 && tecrease >= 0)
            {
                miggle -= (tecrease/kreasetimestart)*2;
                kreasetime -= 1;
            }


            if (kreasetimestart <= 0 && jnecrease >= 0)
            {
                kreasetime = 1;
                kreasetimestart = 1;
            }

            if (miggle >= 0 && kreasetime >= 1 && jnecrease >= 0)
            {
                miggle += jnecrease / kreasetimestart;
                kreasetime -= 1;
            }

            around.position = (start-new Vector3 (0,0,0)) + Random.insideUnitSphere * (miggle);
        }
 
    }
}
