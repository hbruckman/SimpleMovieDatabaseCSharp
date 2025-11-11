import { $, apiFetch, renderStatus, fetchAllGenres, populateGenresSelect, captureMovieForm } from '/scripts/common.js';

(async function initMovieAdd() {
  const form = $('#movie-form');
  const statusEl = $('#status');
  const genresSelect = $('#genres-select');

  try {
    const genres = await fetchAllGenres();
    populateGenresSelect(genresSelect, genres, []);
    renderStatus(statusEl, 'ok', 'Genres loaded. Fill the form and submit.');
  } catch (err) {
    renderStatus(statusEl, 'err', `Failed to load genres: ${err.message}`);
  }

  form.addEventListener('submit', async (ev) => {
    ev.preventDefault();
    const payload = captureMovieForm(form);
    try {
      const created = await apiFetch('/movies', { method: 'POST', body: JSON.stringify(payload) });
      renderStatus(statusEl, 'ok', `Created movie #${created.id} (${created.title}).`);
      form.reset();
    } catch (err) {
      renderStatus(statusEl, 'err', `Create failed: ${err.message}`);
    }
  });
})();
