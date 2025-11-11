import { $, apiFetch, renderStatus, getQueryParam, formatGenres } from '/scripts/common.js';

(async function initMovieView() {
  const id = getQueryParam('id');
  const statusEl = $('#status');
  if (!id) return renderStatus(statusEl, 'err', 'Missing ?id in URL.');

  try {
    const m = await apiFetch(`/movies/${encodeURIComponent(id)}`);
    $('#movie-id').textContent = m.id;
    $('#movie-title').textContent = m.title;
    $('#movie-year').textContent = m.year;
    $('#movie-rating').textContent = Number(m.rating ?? 0).toFixed(1);
    $('#movie-desc').textContent = m.description || '—';
    $('#movie-genres').textContent = formatGenres(m.genres);
    $('#edit-link').href = `/movies/edit.html?id=${encodeURIComponent(m.id)}`;
    renderStatus(statusEl, '', '');
  } catch (err) {
    renderStatus(statusEl, 'err', `Failed to load movie ${id}: ${err.message}`);
  }
})();
