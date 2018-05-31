﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BezierSpline))]
public class TubeWithBezier : MonoBehaviour {
	public float radius;
	public int circleVertices;
	public List<float> rings;
	private Vector3[] vertices;
	
	private Mesh mesh;
	BezierSpline spline;

	public float MaxAngle, jumps;

	void Awake(){
		
	}

	private IEnumerator Generate () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";
		rings.Clear();
		while(transform.childCount > 0){
			GameObject.DestroyImmediate(transform.GetChild(transform.childCount-1));
		}


		Vector3 LastDirection = spline.GetVelocity(0).normalized;
		float t = 0;
		while(t < 1){
			t += jumps;
			if(MaxAngle < Vector3.Angle(LastDirection, spline.GetVelocity(t).normalized)){
				//rings.Add(Vector3.Angle(LastDirection, spline.GetVelocity(t).normalized));	
				rings.Add(t);
				LastDirection = spline.GetVelocity(t).normalized;
			}
			//
		}

		float x, y, angle = 0;

		vertices = new Vector3[circleVertices * (rings.Count + 1)];
		yield return null;
		
		Vector2[] uv = new Vector2[vertices.Length];
		for(int j = 0; j < rings.Count + 1; j++){
			int start = j * circleVertices;
			Transform tr = new GameObject("CurvePoint").transform;
			Vector3 direction = spline.GetVelocity(rings[j]).normalized;
			tr.rotation = Quaternion.LookRotation(direction);
			
			Vector3 point = spline.GetPoint(t) - transform.position;
			tr.parent = transform;
			tr.position = point;
			for(int i = start; i < start + circleVertices; i++){
				x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
				y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
				//print(point);
				vertices[i] = tr.TransformPoint(new Vector3(x,y,0)); 
				uv[i] = new Vector2((float)i / circleVertices, 0);
				angle += 360 / circleVertices;
				//yield return new WaitForSecondsRealtime(0.01f);
			}
		}
		/*
		mesh.vertices = vertices;
		int[] triangles = new int[(circleVertices) * (rings.Count + 1) * 6];
		int vert = 0, vertRef = 0, circle = 0, total = 0;
		for(int r = 0; r < rings.Count; r++){
			circle = r * circleVertices;
			string result;
			for(vertRef = 0; vertRef < circleVertices - 1; vertRef++){
				vert = (vertRef * 6) + (circleVertices * 6) * r;
				result = "[";
				result += triangles[(0 + vert)] = vertRef + circle;
				result += triangles[(1 + vert)] = vertRef + 1 + circle;
				result += triangles[(2 + vert)] = circleVertices + vertRef + circle;
				result += "][";
				result += triangles[(3 + vert)] = vertRef + 1 + circle;
				result += triangles[(5 + vert)] = circleVertices + vertRef + circle;
				result += triangles[(4 + vert)] = vertRef + circleVertices + 1 + circle;
				//print(result + "]");
			}
			vert = (vertRef * 6) + (circleVertices * 6) * r;
			result = "[";
			result += triangles[(2 + vert)] = vertRef + circle;
			result += triangles[(1 + vert)] = vertRef + 1 + circle;
			result += triangles[(0 + vert)] = circle;
			result += "][";
			result += triangles[(4 + vert)] = vertRef + 1 + circle;
			result += triangles[(5 + vert)] = circleVertices + vertRef + circle;
			result += triangles[(3 + vert)] = vertRef + circle;
			//print(result + "]");
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();

		mesh.uv = uv;
		*/
	}

	public void CreateTube(){
		spline = GetComponent<BezierSpline>();
		StartCoroutine(Generate());
	}

	private void OnDrawGizmos () {
		if (vertices == null) {
			return;
		}
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere(vertices[i], 0.02f);
		}
	}
}
