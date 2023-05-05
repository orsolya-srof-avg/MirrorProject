using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Transform startPosition;
    public LaserBeamObjectPool laserBeamObjectPool;
    public PuzzleManager puzzleManager;
    
    private List<LaserBeam> _laserBeams = new List<LaserBeam>();
    private List<Vector3> _laserPoints = new List<Vector3>();
    private List<Color> _colors = new List<Color>();

    private Vector3 _startDirection;
    private Color _lastUsedColor = Color.white;

    private const int _rayDistance = 30;

    private void FixedUpdate() {
        ShootLaserRay(startPosition.position, startPosition.forward);
    }

    void ShootLaserRay(Vector3 startPos, Vector3 direction) {
        _colors.Add(_lastUsedColor);
        _laserPoints.Add(startPos);

        Ray ray = new Ray(startPos, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayDistance)) {
            CheckRaycastHit(hit, direction);
        } else {
            _laserPoints.Add(ray.GetPoint(_rayDistance));
            DrawRay();
        }
    }

    private void CheckRaycastHit(RaycastHit hitInfo, Vector3 direction) {
        if (hitInfo.collider.gameObject.tag == "Mirror") {
            ColorFilter filter = hitInfo.collider.gameObject.GetComponentInParent<ColorFilter>();
            _lastUsedColor = filter.FilterColorRGB(_lastUsedColor);
            _startDirection = Vector3.Reflect(direction, hitInfo.normal);
            ShootLaserRay(hitInfo.point, _startDirection);
        } else if (hitInfo.collider.gameObject.tag == "Button" && !puzzleManager.PuzzleSolved) {
            puzzleManager.CheckPuzzleSolved(_lastUsedColor);
            _laserPoints.Add(hitInfo.point);
            DrawRay();
        }
        else {
            _laserPoints.Add(hitInfo.point);
            DrawRay();
        }
    }

    private void DrawRay() {
        ClearLaserBeams();

        for (int i = 0; i < _laserPoints.Count - 1; i++) {
            LaserBeam beam = laserBeamObjectPool.ObjectPool.Get();
            beam.Setup(_laserPoints[i], _laserPoints[i + 1], _colors[i]);
            _laserBeams.Add(beam);
        }

        _laserPoints.Clear();
        _colors.Clear();
        _lastUsedColor = Color.white;
    }

    private void ClearLaserBeams() {
        foreach (LaserBeam beam in _laserBeams) {
            laserBeamObjectPool.ReleaseLaserBeam(beam);
        }
        _laserBeams.Clear();
    }
}
